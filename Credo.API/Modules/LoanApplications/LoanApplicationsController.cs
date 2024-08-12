using AutoMapper;
using Credo.API.Helpers;
using Credo.API.Modules.LoanApplications.Models;
using Credo.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Application.Modules.LoanApplication.Queries;
using Credo.Domain.ValueObjects;

namespace Credo.API.Modules.LoanApplications;
[Route("Loan-applications")]
[ApiController]
[Authorize]
public class LoanApplicationsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly ILogger<LoanApplicationsController> _logger;

    public LoanApplicationsController(ISender sender, ILogger<LoanApplicationsController> logger, IMapper mapper)
    {
        _sender = sender;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<LoanApplicationQueryDto>> GetApplicationAsync([FromRoute] int id,
        [FromQuery] bool withUser = false)
    {
        var command = new GetApplicationByIdQuery { Id = id, WithUser = withUser };
        var application = await _sender.Send(command);
        if (application is null)
        {
            return NotFound("Application not found.");
        }
        return Ok(_mapper.Map<LoanApplicationQueryDto>(application));
    }


    [HttpPost("Create")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateApplicationAsync([FromBody] CreateLoanApplicationDto application, [FromServices] IValidator<CreateLoanApplicationDto> validator)
    {
        var validationResult = await validator.ValidateAsync(application);
        if (!validationResult.IsValid)
        {
            return new ValidationBadRequestResponse(validationResult, _logger);
        }
        var loanApplication = _mapper.Map<LoanApplication>(application);
        var userId = GetCurrentUserId();
        if (userId is null)
        {
            return StatusCode(401);
        }

        loanApplication.UserId = (int)userId;
        var command = _mapper.Map<CreateLoanApplicationCommand>(loanApplication);
        try
        {
            await _sender.Send(command);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occured while creating loan application. Exception: {@Exception}", ex);
            return Conflict("Something unexpected happened, try again later.");
        }

        return Ok();
    }

    private int? GetCurrentUserId()
    {
        var parsed = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
        if (!parsed)
        {
            return null;
        }
        return userId;
    }
}
