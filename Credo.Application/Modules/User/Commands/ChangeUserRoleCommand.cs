using MediatR;

namespace Credo.Application.Modules.User.Commands;

public class ChangeUserRoleCommand : IRequest
{
    public int Id { get; set; }
    public required string Role { get; set; }
}
