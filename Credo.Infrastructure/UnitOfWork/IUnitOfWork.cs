using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;

namespace Credo.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    LoanApplicationsService LoanApplicationsService { get; }
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}
