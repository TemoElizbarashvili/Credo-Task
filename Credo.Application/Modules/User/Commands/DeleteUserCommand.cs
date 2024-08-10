using MediatR;

namespace Credo.Application.Modules.User.Commands;

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}
