using Credo.Domain.Repositories;

namespace Credo.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ILoanApplicationRepository LoanApplications { get; }
    Task<int> CompleteAsync();
}
