using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;
using Credo.Infrastructure.Repositories;

namespace Credo.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IUserRepository Users { get; }
    public ILoanApplicationRepository LoanApplications { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Users = new UserRepository(context);
        LoanApplications = new LoanApplicationRepository(context);
    }

    public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
