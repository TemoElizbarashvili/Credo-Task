using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(int id);
}
