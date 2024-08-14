using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class ChangeUserPasswordCommandHandler : UserRequestsHandlerBase, IRequestHandler<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await UserRepository.GetByUserNameAsync(request.UserName);
        ArgumentNullException.ThrowIfNull(user);
        await UserRepository.ChangeUserPasswordAsync(user.Id, request.Password);
    }
}
