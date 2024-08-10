using AutoMapper;
using Credo.API.Modules.User.Models;
using Credo.Application.Modules.User.Queries;
using Credo.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Modules.User;
[Route("[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UsersController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] int id)
    {
        var user = await _sender.Send(new GetUserByIdQuery { Id = id });

        if (user is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<PagedList<UserDto>>> PagedListAsync([FromQuery] int pageNumber,
        [FromQuery] int pageSize, [FromQuery] bool showDrafts = false)
    {
        if (showDrafts == true && User.IsInRole("Customer"))
        {
            return Forbid();
        }

        var query = new UsersPagedListQuery { PageNumber = pageNumber, PageSize = pageSize, ShowDrafts = showDrafts };

        var usersPagedList = await _sender.Send(query);

        return Ok(_mapper.Map<PagedList<UserDto>>(usersPagedList));
    }

}
