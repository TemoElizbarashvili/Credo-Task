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
}
