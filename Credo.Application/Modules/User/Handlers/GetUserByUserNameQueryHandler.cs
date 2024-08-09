using MediatR;
using Credo.Application.Modules.User.Queries;
using Credo.Application.Modules.Shared;
using Credo.Infrastructure.UnitOfWork;

namespace Credo.Application.Modules.User.Handlers;

public class GetUserByUserNameQueryHandler : BaseHandler, IRequestHandler<GetUserByUserNameQuery, Domain.Entities.User?>
{
    public GetUserByUserNameQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public async Task<Domain.Entities.User?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        => await _unitOfWork.UsersService.GetByUserNameAsync(request.UserName);
    
}
