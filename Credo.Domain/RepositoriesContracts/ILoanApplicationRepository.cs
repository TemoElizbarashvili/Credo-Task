using Credo.Common.Models;
using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface ILoanApplicationRepository
{
    Task AddAsync(LoanApplication loanApplication);
    Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false);
    Task UpdateAsync(LoanApplication loanApplication);

    Task AddRangeAsync(IEnumerable<LoanApplication> applications);

    public Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(LoanApplicationQueryParameters query,
        bool withUser = false);
}
