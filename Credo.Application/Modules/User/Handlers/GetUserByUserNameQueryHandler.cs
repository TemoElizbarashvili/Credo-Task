using MediatR;
using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;

namespace Credo.Application.Modules.User.Handlers;

public class GetUserByUserNameQueryHandler : UserRequestsHandlerBase, IRequestHandler<GetUserByUserNameQuery, Domain.Entities.User?>
{
    public GetUserByUserNameQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        => await _userRepository.GetByUserNameAsync(request.UserName);
}
