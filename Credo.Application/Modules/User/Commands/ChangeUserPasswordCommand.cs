using MediatR;

namespace Credo.Application.Modules.User.Commands;

public class ChangeUserPasswordCommand : IRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
