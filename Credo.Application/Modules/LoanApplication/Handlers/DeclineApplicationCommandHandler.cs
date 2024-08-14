using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Common.Models;
using Credo.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class DeclineApplicationCommandHandler : LoanApplicationBaseRequestHandler, IRequestHandler<DeclineApplicationCommand>
{
    public DeclineApplicationCommandHandler(LoanApplicationsService loanApplicationsService, ILogger<LoanApplicationBaseRequestHandler> logger) : base(loanApplicationsService, logger)
    {
    }

    public async Task Handle(DeclineApplicationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await LoanApplicationsService.ProcessLoanApplicationAsync(request.Id, LoanStatus.Declined);
        }
        catch (Exception ex)
        {
            Logger.LogError("Error occured while declining application with ID - {@ID}, Exception - {@Exception}", request.Id, ex);
            throw;
        }
    }
}
