using AutoMapper;
using Credo.API.Helpers;
using Credo.API.Modules.Auth.Models;
using Credo.Application.Modules.User.Commands;
using Credo.Application.Modules.User.Queries;
using Credo.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Modules.Auth;

[Route("Auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ISender sender, IMapper mapper, ILogger<AuthController> logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("Login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginDto login, [FromServices] IConfiguration config)
    {
        var user = await _sender.Send(new GetUserByUserNameQuery { UserName = login.UserName });
        if (user is null)
        {
            return BadRequest("UserName or password is invalid!");
        }

        if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            return BadRequest("User name or password is invalid!");

        var token = await AuthHelper.GenerateTokenAsync(user, config);

        return Ok(token);
    }

    [HttpPost("Register")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto, [FromServices] IValidator<UserRegisterDto> validator)
    {
        // Only Manager can Add user with Role "Manager
        if (userDto.Role == UserRole.Manager)
        {
            if (!User.IsInRole("Manager"))
            {
                return Forbid();
            }
        }

        var validationResult = await validator.ValidateAsync(userDto);

        if (!validationResult.IsValid)
        {
            return new ValidationBadRequestResponse(validationResult, _logger);
        }

        var user = _mapper.Map<Domain.Entities.User>(userDto);
        user.Password = await AuthHelper.HashPasswordAsync(user.Password);
        try
        {
            await _sender.Send(new CreateUserCommand { User = user });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.InnerException });
        }

        return Ok();
    }

    [HttpPatch("Change-password")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register(ChangePasswordDto dto, [FromServices] IValidator<ChangePasswordDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new ValidationBadRequestResponse(validationResult, _logger);
        }

        var passwordHash = await AuthHelper.HashPasswordAsync(dto.Password);
        var command = new ChangeUserPasswordCommand { Password = passwordHash, UserName = dto.UserName };
        try
        {
            await _sender.Send(command);
        }
        catch (ArgumentNullException)
        {
            return BadRequest($"User with userName {dto.UserName} can not be found.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception occured while changing password of user with userName {@UserName}, exception: {@Exception}", dto.UserName, ex.InnerException);
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
        return Ok();
    }
}
