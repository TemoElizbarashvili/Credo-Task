using MediatR;

namespace Credo.Application.Queries.User;

public class GetUserByIdQuery : IRequest<Domain.Entities.User?>
{
    public required int Id { get; set; }
}
