using Credo.Domain.Services;
using System.Transactions;

namespace Credo.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
