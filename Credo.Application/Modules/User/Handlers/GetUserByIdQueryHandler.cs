using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;
using MediatR;
using Serilog;

namespace Credo.Application.Modules.User.Handlers;


public class GetUserByIdQueryHandler : UserRequestsHandlerBase, IRequestHandler<GetUserByIdQuery, Domain.Entities.User?>
{
    public GetUserByIdQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            return user;
        }
        catch (Exception ex)
        {
            Log.Error($"Exception Happened while getting User. User Id: {request.Id}, Message: {ex.Message}");
            throw;
        }
        return null;
    }
}
