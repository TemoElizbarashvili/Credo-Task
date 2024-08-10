using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;

public class ChangeUserRoleCommandHandler : UserRequestsHandlerBase, IRequestHandler<ChangeUserRoleCommand>
{
    public ChangeUserRoleCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ChangeUserRoleAsync(request.Id, request.Role);
    }
}
