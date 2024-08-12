using Credo.Common.Models;
using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface ILoanApplicationRepository
{
    Task AddAsync(LoanApplication loanApplication);
    Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false);
    Task UpdateAsync(LoanApplication loanApplication);
    public int GetNextId();

    public Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(
        int pageNumber,
        int pageSize,
        int? userId = null,
        string? currency = null,
        string? loanType = null,
        string? loanStatus = null,
        decimal? minAmount = null,
        decimal? maxAmount = null);
}
