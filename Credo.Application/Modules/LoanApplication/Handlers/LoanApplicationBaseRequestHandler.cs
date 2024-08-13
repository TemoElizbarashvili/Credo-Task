using Credo.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class LoanApplicationBaseRequestHandler
{
    protected readonly LoanApplicationsService _loanApplicationsService;
    protected readonly ILogger<LoanApplicationBaseRequestHandler> _logger;

    public LoanApplicationBaseRequestHandler(
        LoanApplicationsService loanApplicationsService,
        ILogger<LoanApplicationBaseRequestHandler> logger)
    {
        _loanApplicationsService = loanApplicationsService;
        _logger = logger;
    }

}
