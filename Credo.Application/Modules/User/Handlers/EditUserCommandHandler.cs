using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;

public class EditUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<EditUserCommand>
{
    public EditUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        => await _userRepository.EditUserAsync(request.User);
}
