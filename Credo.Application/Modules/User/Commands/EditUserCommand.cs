using MediatR;

namespace Credo.Application.Modules.User.Commands;

public class EditUserCommand : IRequest
{
    public required Domain.Entities.User User { get; set; }
}
