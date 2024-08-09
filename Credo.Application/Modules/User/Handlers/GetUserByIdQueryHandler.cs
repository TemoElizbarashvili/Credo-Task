using Credo.Application.Modules.Shared;
using Credo.Application.Modules.User.Queries;
using Credo.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace Credo.Application.Modules.User.Handlers;

public class GetUserByIdQueryHandler : BaseHandler, IRequestHandler<GetUserByIdQuery, Domain.Entities.User?>
{

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.UsersService.GetByIdAsync(request.Id);

            return user;
        }
        catch (Exception ex)
        {
            Log.Error($"Exception Happened while getting User. User Id: {request.Id}, Message: {ex.Message}");
        }
        return null;
    }
}
