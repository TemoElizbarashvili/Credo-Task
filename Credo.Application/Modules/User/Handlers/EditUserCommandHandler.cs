using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class EditUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<EditUserCommand>
{
    public EditUserCommandHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
        => await UserRepository.EditUserAsync(request.User);
}
