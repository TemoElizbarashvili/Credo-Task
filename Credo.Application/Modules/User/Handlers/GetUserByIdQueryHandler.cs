using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.User.Handlers;

public class GetUserByIdQueryHandler : UserRequestsHandlerBase, IRequestHandler<GetUserByIdQuery, Domain.Entities.User?>
{
    public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<UserRequestsHandlerBase> logger) : base(userRepository, logger) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await UserRepository.GetByIdAsync(request.Id);

            return user;
        }
        catch (Exception ex)
        {
            Logger.LogError("Exception Happened while getting User. User Id: {@RequestId}, Message: {@Exception}", request.Id, ex.Message);
            throw;
        }
    }
}
