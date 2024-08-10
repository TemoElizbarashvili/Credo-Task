using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;
using Credo.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task<PagedList<User>> UsersPagedListAsync(int pageNumber, int pageSize, bool showDrafts = false)
    {
        var totalItems = await _context.Users.CountAsync();

        var users = _context.Users.AsQueryable();
        if (!showDrafts)
        {
            users = users.Where(u => u.IsDeleted == false);
        }

        var items = await users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<User>(items, totalItems, pageNumber, pageSize);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user!;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(userName));

        return user;
    }

    public async Task<bool> IsUserNameUniqueAsync(string userName)
    {
        return !await _context.Users.AnyAsync(x => x.UserName == userName);
    }
}
