using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.ValueObjects;

namespace Credo.Domain.Aggregates;

public class LoanApplicationAggregate
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public LoanApplicationAggregate(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false)
        => await _loanApplicationRepository.GetByIdAsync(id, withUser);

    public async Task CreateLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationRepository.AddAsync(loanApplication);

    public int GetNextId()
        => _loanApplicationRepository.GetNextId();

    public async Task UpdateLoanApplicationStatusAsync(int loanApplicationId, LoanStatus newStatus)
    {
        var loanApplication = await _loanApplicationRepository.GetByIdAsync(loanApplicationId);
        if (loanApplication is not null)
        {
            loanApplication.UpdateStatus(newStatus);
            await _loanApplicationRepository.UpdateAsync(loanApplication);
        }
    }
}
