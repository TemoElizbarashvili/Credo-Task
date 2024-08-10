using Credo.Domain.ValueObjects;

namespace Credo.API.Modules.Auth.Models;

public class UserRegisterDto
{
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string PersonalNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required UserRole Role { get; set; }
}
