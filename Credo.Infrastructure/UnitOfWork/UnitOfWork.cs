using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Infrastructure.DB;
using Credo.Infrastructure.Repositories;

namespace Credo.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UsersService UsersService { get; }
    public LoanApplicationsService LoanApplicationsService { get; }

    public UnitOfWork(ApplicationDbContext context, UsersService usersService, LoanApplicationsService loanApplicationsService)
    {
        _context = context;
        UsersService = usersService;
        LoanApplicationsService = loanApplicationsService;
    }

    public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

    public void Dispose()
        => _context.Dispose();
}
