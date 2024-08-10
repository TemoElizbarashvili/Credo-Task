using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Serilog;

namespace Credo.Application.Modules.User.Handlers;

public class CreateUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<CreateUserCommand, int>
{
    public CreateUserCommandHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _userRepository.AddAsync(request.User);
        }
        catch (Exception ex)
        {
            Log.Error($"Exception Happened while creating User. Message: {ex.Message}");
        }

        return request.User.Id;
    }
}
