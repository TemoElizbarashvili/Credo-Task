using Credo.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class LoanApplicationBaseRequestHandler
{
    protected readonly LoanApplicationsService LoanApplicationsService;
    protected readonly ILogger<LoanApplicationBaseRequestHandler> Logger;

    public LoanApplicationBaseRequestHandler(
        LoanApplicationsService loanApplicationsService,
        ILogger<LoanApplicationBaseRequestHandler> logger)
    {
        LoanApplicationsService = loanApplicationsService;
        Logger = logger;
    }

}
