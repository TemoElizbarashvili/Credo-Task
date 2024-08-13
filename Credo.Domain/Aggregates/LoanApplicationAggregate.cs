using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;

namespace Credo.Domain.Aggregates;

public class LoanApplicationAggregate
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public LoanApplicationAggregate(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(LoanApplicationQueryParameters query, bool withUser = false)
        => await _loanApplicationRepository.ApplicationsPagedListAsync(query, withUser);

    public async Task AddRangeAsync(IEnumerable<LoanApplication> loanApplications)
        => await _loanApplicationRepository.AddRangeAsync(loanApplications);

    public async Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false)
        => await _loanApplicationRepository.GetByIdAsync(id, withUser);

    public async Task CreateLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationRepository.AddAsync(loanApplication);

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
