using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface ILoanApplicationRepository
{
    Task AddAsync(LoanApplication loanApplication);
    Task<LoanApplication?> GetByIdAsync(int id);
    Task UpdateAsync(LoanApplication loanApplication);
}
