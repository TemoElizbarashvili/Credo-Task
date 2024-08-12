using Credo.Domain.Services;
using Credo.Infrastructure.DB;
using Microsoft.EntityFrameworkCore.Storage;

namespace Credo.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(ApplicationDbContext context, LoanApplicationsService loanApplicationsService)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            _currentTransaction.Dispose();
            _currentTransaction = null!;
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}
