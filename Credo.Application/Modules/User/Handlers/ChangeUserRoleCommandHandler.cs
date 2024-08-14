using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class ChangeUserRoleCommandHandler : UserRequestsHandlerBase, IRequestHandler<ChangeUserRoleCommand>
{
    public ChangeUserRoleCommandHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        await UserRepository.ChangeUserRoleAsync(request.Id, request.Role);
    }
}
