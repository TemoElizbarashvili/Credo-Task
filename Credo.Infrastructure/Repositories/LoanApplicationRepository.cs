using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.Repositories;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public LoanApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(LoanApplication loanApplication)
        => await Task.FromResult(_context.LoanApplications.Add(loanApplication));

    public async Task<LoanApplication?> GetByIdAsync(int id, bool withUser = false)
    {
        var applicationQuery = _context.LoanApplications.Where(x => x.Id == id).AsQueryable();

        if (withUser)
        {
            applicationQuery = applicationQuery.Include(x => x.User);
        }

        var application = await applicationQuery.SingleOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(application, nameof(application));

        return application;
    }

    public async Task UpdateAsync(LoanApplication loanApplication)
        => await Task.FromResult(_context.LoanApplications.Update(loanApplication));

    public async Task AddRangeAsync(IEnumerable<LoanApplication> applications)
        => await _context.LoanApplications.AddRangeAsync(applications);

    public async Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(LoanApplicationQueryParameters query, bool withUser = false)
    {
        var totalItems = await _context.LoanApplications.CountAsync();

        var applications = _context.LoanApplications.AsQueryable();
        if (withUser)
        {
            applications = applications.Include(a => a.User);
        }
        if (query.UserId is not null)
        {
            applications = applications.Where(a => a.UserId == query.UserId);
        }
        if (!string.IsNullOrEmpty(query.Currency))
        {
            applications = applications.Where(a => a.Currency == query.Currency);
        }
        if (!string.IsNullOrEmpty(query.LoanType))
        {
            applications = applications.Where(a => a.LoanType == (LoanType)Enum.Parse(typeof(LoanType), query.LoanType));
        }
        if (!string.IsNullOrEmpty(query.LoanStatus))
        {
            applications = applications.Where(a => a.Status == (LoanStatus)Enum.Parse(typeof(LoanStatus), query.LoanStatus));
        }
        if (query.MinAmount is not null)
        {
            applications = applications.Where(a => a.Amount >= query.MinAmount);
        }
        if (query.MaxAmount is not null)
        {
            applications = applications.Where(a => a.Amount <= query.MaxAmount);
        }

        var items = await applications
            .Skip((query.PageNumber- 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedList<LoanApplication>(items, totalItems, query.PageNumber, query.PageSize);
    }
}
