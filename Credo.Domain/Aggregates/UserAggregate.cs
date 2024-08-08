using Credo.Domain.Entities;
using Credo.Domain.Repositories;

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
}
