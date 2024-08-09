using System.Runtime.CompilerServices;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;

namespace Credo.Domain.Aggregates;

public class UserAggregate
{
    private readonly IUserRepository _userRepository;

    public UserAggregate(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }

    public async Task<User?> GetByIdAsync(int id)
        => await _userRepository.GetByIdAsync(id);

    public async Task<User?> GetByUserNameAsync(string userName)
        => await _userRepository.GetByUserNameAsync(userName);
}
