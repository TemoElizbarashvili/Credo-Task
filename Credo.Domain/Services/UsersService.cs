using Credo.Domain.Aggregates;
using Credo.Domain.Entities;

namespace Credo.Domain.Services;

public class UsersService
{
    private readonly UserAggregate _userAggragete;

    public UsersService(UserAggregate userAggregate)
    {
        _userAggragete = userAggregate;
    }

    public async Task RegisterUserAsync(User user)
        => await _userAggragete.RegisterUserAsync(user);

    public async Task<User?> GetByIdAsync(int id)
        => await _userAggragete.GetByIdAsync(id);

    public async Task<User?> GetByUserNameAsync(string userName)
        => await _userAggragete.GetByUserNameAsync(userName);
}
