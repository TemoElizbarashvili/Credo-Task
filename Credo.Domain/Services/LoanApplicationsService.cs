using Credo.Domain.Aggregates;
using Credo.Domain.Entities;
using Credo.Domain.ValueObjects;

namespace Credo.Domain.Services;

//Overkill, For Demonstration purposes only
public class LoanApplicationsService
{
    private readonly LoanApplicationAggregate _loanApplicationAggregate;

    public LoanApplicationsService(LoanApplicationAggregate loanApplicationAggregate)
    {
        _loanApplicationAggregate = loanApplicationAggregate;
    }

    public async Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false)
        => await _loanApplicationAggregate.GetByIdAsync(id, withUser);

    public async Task CreateLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationAggregate.CreateLoanApplicationAsync(loanApplication);

    public int GetNextId()
        => _loanApplicationAggregate.GetNextSequenceId();

    public async Task ProcessLoanApplicationAsync(int loanApplicationId, LoanStatus newStatus)
        => await _loanApplicationAggregate.UpdateLoanApplicationStatusAsync(loanApplicationId, newStatus);
}
