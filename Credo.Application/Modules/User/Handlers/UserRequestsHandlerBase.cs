using Credo.Domain.RepositoriesContracts;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class UserRequestsHandlerBase
{
    protected readonly IUserRepository UserRepository;
    protected readonly ILogger<UserRequestsHandlerBase> Logger;

    public UserRequestsHandlerBase(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger)
    {
        UserRepository = userRepository;
        Logger = logger;
    }
}
