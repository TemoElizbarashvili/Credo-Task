using Credo.Domain.Aggregates;
using Credo.Domain.Entities;
using Credo.Domain.ValueObjects;

namespace Credo.Domain.Services;

public class LoanApplicationsService
{
    private readonly LoanApplicationAggregate _loanApplicationAggregate;

    public LoanApplicationsService(LoanApplicationAggregate loanApplicationAggregate)
    {
        _loanApplicationAggregate = loanApplicationAggregate;
    }

    public async Task SubmitLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationAggregate.SubmitLoanApplicationAsync(loanApplication);

    public async Task ProcessLoanApplicationAsync(int loanApplicationId, LoanStatus newStatus)
        => await _loanApplicationAggregate.UpdateLoanApplicationStatusAsync(loanApplicationId, newStatus);
}
