using MediatR;

namespace Credo.Application.Queries.User;

public class GetUserByIdQuery : IRequest<Domain.Entities.User?>
{
    required public int Id { get; set; }
}
