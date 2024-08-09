using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;

namespace Credo.Infrastructure.Repositories;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public LoanApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LoanApplication loanApplication)
        => await _context.AddAsync(loanApplication);

    public async Task<LoanApplication?> GetByIdAsync(int id)
    {
        var application = await _context.LoanApplications.FindAsync(id);
        ArgumentNullException.ThrowIfNull(application, nameof(application));
        return application!;
    }

    public async Task UpdateAsync(LoanApplication loanApplication)
        => await Task.FromResult(_context.LoanApplications.Update(loanApplication));
}
