using Credo.Domain.Entities;

namespace Credo.Domain.Repositories;

public interface ILoanApplicationRepository
{
    Task AddAsync(LoanApplication loanApplication);
    Task<LoanApplication> GetByIdAsync(int id);
    Task UpdateAsync(LoanApplication loanApplication);
}
