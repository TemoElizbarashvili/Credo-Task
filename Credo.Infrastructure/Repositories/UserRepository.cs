using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;
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

    public async Task ChangeUserRoleAsync(int id, string role)
    {
        var user = await _context.Users.FindAsync(id);
        ArgumentNullException.ThrowIfNull(user);
        user.Role = role;
        _context.Users.Update(user);
    }

    public async Task ChangeUserPasswordAsync(int id, string newPassword)
    {
        var user = await _context.Users.FindAsync(id);
        user!.Password = newPassword;
        _context.Users.Update(user);
    }

    public async Task<PagedList<User>> UsersPagedListAsync(
        int pageNumber,
        int pageSize,
        string? userName = null,
        string? firstName = null,
        string? lastName = null,
        string? personNumber = null,
        bool showDrafts = false)
    {
        var totalItems = await _context.Users.CountAsync();

        var users = _context.Users.AsQueryable();
        if (!showDrafts)
        {
            users = users.Where(u => u.IsDeleted == false);
        }
        if (!string.IsNullOrEmpty(userName))
        {
            users = users.Where(u => u.UserName.ToLower().Contains(userName.ToLower()));
        }
        if (!string.IsNullOrEmpty(firstName))
        {
            users = users.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower()));
        }
        if (!string.IsNullOrEmpty(lastName))
        {
            users = users.Where(u => u.LastName.ToLower().Contains(lastName.ToLower()));
        }
        if (!string.IsNullOrEmpty(personNumber))
        {
            users = users.Where(u => u.PersonalNumber.ToLower().Contains(personNumber.ToLower()));

        }

        var items = await users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<User>(items, totalItems, pageNumber, pageSize);
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.SingleOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

    public async Task<User?> GetByUserNameAsync(string userName)
        => await _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(userName) && !u.IsDeleted);

    public async Task<bool> IsUserNameUniqueAsync(string userName)
        => !await _context.Users.AnyAsync(x => x.UserName == userName && !x.IsDeleted);

    public Task DeleteUserAsync(User user)
    {
        user.IsDeleted = true;
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task EditUserAsync(User user)
    {
        var oldUser = await _context.Users.FindAsync(user.Id);
        user.Password = oldUser!.Password;
        user.Role = oldUser.Role;
        user.IsDeleted = oldUser.IsDeleted;
        _context.Users.Update(user);
    }
}
