using Credo.Domain.RepositoriesContracts;

namespace Credo.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    ILoanApplicationRepository LoanApplications { get; }
    Task<int> CompleteAsync();
}
