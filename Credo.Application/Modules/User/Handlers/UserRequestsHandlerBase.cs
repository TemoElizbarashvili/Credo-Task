using Credo.Domain.RepositoriesContracts;

namespace Credo.Application.Modules.User.Handlers;

public class UserRequestsHandlerBase
{
    protected readonly IUserRepository _userRepository;

    public UserRequestsHandlerBase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}
