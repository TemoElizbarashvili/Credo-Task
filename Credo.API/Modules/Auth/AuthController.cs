using AutoMapper;
using Azure.Core;
using Credo.API.Helpers;
using Credo.API.Modules.Auth.Models;
using Credo.Application.Modules.User.Commands;
using Credo.Application.Modules.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Modules.Auth;

[Microsoft.AspNetCore.Components.Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginDto login, [FromServices] IConfiguration config)
    {
        var user = await _mediator.Send(new GetUserByUserNameQuery { UserName = login.UserName });
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
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _mapper.Map<Domain.Entities.User>(userDto);
        user.Password = await AuthHelper.HashPasswordAsync(user.Password);
        try
        {
            await _mediator.Send(new CreateUserCommand { User = user });
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.InnerException?.Message });
        }

        return Ok();
    }
}
