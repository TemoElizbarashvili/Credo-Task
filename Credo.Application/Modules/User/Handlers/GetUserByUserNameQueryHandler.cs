using MediatR;
using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class GetUserByUserNameQueryHandler : UserRequestsHandlerBase, IRequestHandler<GetUserByUserNameQuery, Domain.Entities.User?>
{
    public GetUserByUserNameQueryHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        => await UserRepository.GetByUserNameAsync(request.UserName);
}
