using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;
internal class DeleteUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<DeleteUserCommand>
{
    public DeleteUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        ArgumentNullException.ThrowIfNull(user);
        await _userRepository.DeleteUserAsync(user);
    }
}
    