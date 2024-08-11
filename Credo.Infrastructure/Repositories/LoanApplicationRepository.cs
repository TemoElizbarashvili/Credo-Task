using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.ValueObjects;
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
        => await _context.AddAsync(loanApplication);

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

    public async Task<PagedList<LoanApplication>> ApplicationsPagedListAsync(int pageNumber, int pageSize, int? userId = null, string? currency = null,
        string? loanType = null, string? loanStatus = null, decimal? minAmount = null, decimal? maxAmount = null)
    {
        var totalItems = await _context.LoanApplications.CountAsync();

        var applications = _context.LoanApplications.AsQueryable();
        if (userId is not null)
        {
            applications = applications.Where(a => a.UserId == userId);
        }
        if (!string.IsNullOrEmpty(currency))
        {
            applications = applications.Where(a => a.Currency == currency);
        }
        if (!string.IsNullOrEmpty(loanType))
        {
            applications = applications.Where(a => a.LoanType == (LoanType)Enum.Parse(typeof(LoanType), loanType));
        }
        if (!string.IsNullOrEmpty(loanStatus))
        {
            applications = applications.Where(a => a.Status == (LoanStatus)Enum.Parse(typeof(LoanStatus), loanStatus));
        }
        if (minAmount is not null)
        {
            applications = applications.Where(a => a.Amount >= minAmount);
        }
        if (maxAmount is not null)
        {
            applications = applications.Where(a => a.Amount <= maxAmount);
        }

        var items = await applications
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<LoanApplication>(items, totalItems, pageNumber, pageSize);
    }
}
