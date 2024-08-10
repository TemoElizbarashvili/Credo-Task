using Credo.Common.Models;
using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    Task<bool> IsUserNameUniqueAsync(string userName);
    Task DeleteUserAsync(User user);
    Task EditUserAsync(User user);
    Task ChangeUserRoleAsync(int id, string role);
    Task ChangeUserPasswordAsync(int id, string newPassword);

    public Task<PagedList<User>> UsersPagedListAsync(
        int pageNumber,
        int pageSize,
        string? userName = null,
        string? firstName = null,
        string? lastName = null,
        string? personNumber = null,
        bool showDrafts = false);
}
