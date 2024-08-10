using Credo.Domain.Entities;
using Credo.Infrastructure.Models;

namespace Credo.Domain.RepositoriesContracts;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<PagedList<User>> UsersPagedListAsync(int pageNumber, int pageSize, bool showDrafts = false);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    Task<bool> IsUserNameUniqueAsync(string userName);
}
