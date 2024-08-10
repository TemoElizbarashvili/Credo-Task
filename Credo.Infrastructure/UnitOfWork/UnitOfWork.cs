using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Infrastructure.DB;
using Credo.Infrastructure.Repositories;

namespace Credo.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public LoanApplicationsService LoanApplicationsService { get; }

    public UnitOfWork(ApplicationDbContext context, LoanApplicationsService loanApplicationsService)
    {
        _context = context;
        LoanApplicationsService = loanApplicationsService;
    }

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
        => _context.Dispose();
}
