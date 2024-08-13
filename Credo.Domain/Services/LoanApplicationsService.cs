using Credo.Common.Models;
using Credo.Domain.Aggregates;
using Credo.Domain.Entities;

namespace Credo.Domain.Services;

//Overkill, For Demonstration purposes only
public class LoanApplicationsService
{
    private readonly LoanApplicationAggregate _loanApplicationAggregate;

    public LoanApplicationsService(LoanApplicationAggregate loanApplicationAggregate)
    {
        _loanApplicationAggregate = loanApplicationAggregate;
    }

    public async Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(LoanApplicationQueryParameters query, bool withUser = false)
        => await _loanApplicationAggregate.ApplicationsPagedListAsync(query, withUser);

    public async Task AddRangeAsync(IEnumerable<LoanApplication> loanApplications)
        => await _loanApplicationAggregate.AddRangeAsync(loanApplications);

    public async Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false)
        => await _loanApplicationAggregate.GetByIdAsync(id, withUser);

    public async Task CreateLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationAggregate.CreateLoanApplicationAsync(loanApplication);

    public async Task ProcessLoanApplicationAsync(int loanApplicationId, LoanStatus newStatus)
        => await _loanApplicationAggregate.UpdateLoanApplicationStatusAsync(loanApplicationId, newStatus);
}
