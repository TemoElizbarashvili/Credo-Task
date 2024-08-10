using AutoMapper;
using Credo.API.Helpers;
using Credo.API.Modules.User.Models;
using Credo.Application.Modules.User.Commands;
using Credo.Application.Modules.User.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Credo.Common.Models;

namespace Credo.API.Modules.User;
[Route("[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(ISender sender, IMapper mapper, ILogger<UsersController> logger)
    {
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
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
    [ProducesResponseType(403)]
    public async Task<ActionResult<PagedList<UserDto>>> PagedListAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromQuery] string? userName = null,
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null,
        [FromQuery] string? personalNumber = null,
        [FromQuery] bool showDrafts = false)
    {
        if (showDrafts == true && User.IsInRole("Customer"))
        {
            return new CustomForbidResult("You do not have permission to perform this action.");
        }

        var query = new UsersPagedListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            ShowDrafts = showDrafts,
            FirstName = firstName,
            LastName = lastName,
            PersonNumber = personalNumber,
            UserName = userName
        };

        var usersPagedList = await _sender.Send(query);

        return Ok(_mapper.Map<PagedList<UserDto>>(usersPagedList));
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{id:int}/Delete")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
    {
        if (!CanModifyUser(id))
        {
            return Forbid();
        }

        var command = new DeleteUserCommand { Id = id };
        try
        {
            await _sender.Send(command);
        }
        catch (ArgumentNullException)
        {
            return BadRequest($"User with Id {id} can not be found.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception occured while deleting user with Id {@Id}, exception: {@Exception}", id, ex.InnerException);
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

        return NoContent();
    }

    [HttpPut("Edit")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> EditUserAsync([FromBody] EditUserDto user,
        [FromServices] IValidator<EditUserDto> validator)
    {
        if (!CanModifyUser(user.Id))
        {
            return Forbid();
        }

        var validationResult = await validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new ValidationBadRequestResponse(validationResult, _logger);
        }

        var userEntity = _mapper.Map<Domain.Entities.User>(user);
        var command = new EditUserCommand { User = userEntity };
        try
        {
            await _sender.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occured while editing user {@User}, Exception: {@Exception}", user, ex.InnerException);
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
        return Ok();
    }

    [Authorize(Roles = "Manager")]
    [HttpPatch("{id:int}/Change-Role")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ChangeUserRole([FromRoute] int id, UserRolesDto role)
    {
        try
        {
            var command = new ChangeUserRoleCommand { Role = role.ToString(), Id = id };
            await _sender.Send(command);
        }
        catch (ArgumentNullException)
        {
            return BadRequest($"User with Id {id} can not be found.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception occured while changing role of user with Id {@Id}, exception: {@Exception}", id, ex.InnerException);
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
        return Ok();
    }

    private bool CanModifyUser(int id)
    {
        var parsed = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
        if (!parsed)
        {
            return false;
        }
        return userId == id || User.IsInRole("Manager");
    }
}
