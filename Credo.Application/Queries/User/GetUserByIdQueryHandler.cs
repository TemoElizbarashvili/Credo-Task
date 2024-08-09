using Credo.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace Credo.Application.Queries.User;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Credo.Domain.Entities.User?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);

            return user;
        }
        catch (Exception ex)
        {
            Log.Error($"Exception Happened while getting User. User Id: {request.Id}, Message: {ex.Message}");
        }
        return null;
    }
}
