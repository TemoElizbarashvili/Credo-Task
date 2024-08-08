using Credo.Domain.Entities;

namespace Credo.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(int id);
}
