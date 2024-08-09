using AutoMapper;
using Credo.API.Modules.User.Models;
using Credo.Application.Modules.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Modules.User;
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });

        if (user is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserDto>(user));
    }
}
