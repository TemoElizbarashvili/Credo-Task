using Credo.Domain.Entities;
using Credo.Domain.Repositories;
using Credo.Infrastructure.DB;

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

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        ArgumentNullException.ThrowIfNull(nameof(user));
        return user!;
    }
}
