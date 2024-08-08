using MediatR;
using Credo.Domain.Entities;

namespace Credo.Application.Commands.User;

public class CreateUserCommand : IRequest<int>
{
    required public Credo.Domain.Entities.User User { get; set; }
}
