using MediatR;

namespace Credo.Application.Modules.User.Queries;

public class GetUserByIdQuery : IRequest<Domain.Entities.User?>
{
    public required int Id { get; set; }
}


