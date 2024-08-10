using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;

public class ChangeUserPasswordCommandHandler : UserRequestsHandlerBase, IRequestHandler<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserNameAsync(request.UserName);
        ArgumentNullException.ThrowIfNull(user);
        await _userRepository.ChangeUserPasswordAsync(user.Id, request.Password);
    }
}
