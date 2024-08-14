using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Common.Models;
using Credo.Domain.Services;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class SubmitApplicationCommandHandler : LoanApplicationBaseRequestHandler, IRequestHandler<SubmitApplicationCommand>
{

    public SubmitApplicationCommandHandler(LoanApplicationsService loanApplicationsService, ILogger<LoanApplicationBaseRequestHandler> logger) : base(loanApplicationsService, logger)
    {
    }

    public async Task Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _loanApplicationsService.ProcessLoanApplicationAsync(request.Id, LoanStatus.Submitted);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while submitting application with Id {@Id}, Exception - {@Exception}", request.Id, ex);
            throw;
        }
    }
}
