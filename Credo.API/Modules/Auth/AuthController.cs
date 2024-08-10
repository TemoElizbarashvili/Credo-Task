using AutoMapper;
using Credo.API.Helpers;
using Credo.API.Modules.Auth.Models;
using Credo.Application.Modules.User.Commands;
using Credo.Application.Modules.User.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Modules.Auth;

[Microsoft.AspNetCore.Components.Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
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

        var user = _mapper.Map<Domain.Entities.User>(userDto);
        user.Password = await AuthHelper.HashPasswordAsync(user.Password);
        try
        {
            await _sender.Send(new CreateUserCommand { User = user });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.InnerException?.Message });
        }

        return Ok();
    }
}
