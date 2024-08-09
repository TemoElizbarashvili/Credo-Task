using MediatR;
using Credo.Domain.Entities;

namespace Credo.Application.Modules.User.Commands;

public class CreateUserCommand : IRequest<int>
{
    public required Domain.Entities.User User { get; set; }
}
