using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;
internal class DeleteUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<DeleteUserCommand>
{
    public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await UserRepository.GetByIdAsync(request.Id);
        ArgumentNullException.ThrowIfNull(user);
        await UserRepository.DeleteUserAsync(user);
    }
}
    