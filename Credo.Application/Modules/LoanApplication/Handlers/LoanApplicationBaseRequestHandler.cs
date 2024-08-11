using Credo.Domain.Services;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class LoanApplicationBaseRequestHandler
{
    protected readonly LoanApplicationsService _loanApplicationsService;

    public LoanApplicationBaseRequestHandler(LoanApplicationsService loanApplicationsService)
    {
        _loanApplicationsService = loanApplicationsService;
    }
}
