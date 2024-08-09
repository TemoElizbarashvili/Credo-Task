using System.Security.Cryptography.X509Certificates;
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

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user!;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));

        return user;
    }
}
