using Credo.Application.Modules.User.Commands;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class CreateUserCommandHandler : UserRequestsHandlerBase, IRequestHandler<CreateUserCommand, int>
{
    public CreateUserCommandHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UserRepository.AddAsync(request.User);
        }
        catch (Exception ex)
        {
            Logger.LogError("Exception Happened while creating User. Message: {@Message}", ex.Message);
            throw;
        }

        return request.User.Id;
    }
}
