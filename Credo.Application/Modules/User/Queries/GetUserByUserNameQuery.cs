using MediatR;

namespace Credo.Application.Modules.User.Queries;

public class GetUserByUserNameQuery : IRequest<Domain.Entities.User>
{
    public required string UserName { get; set; }
}
