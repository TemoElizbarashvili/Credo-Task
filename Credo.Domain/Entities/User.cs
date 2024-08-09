using Credo.Domain.ValueObjects;

namespace Credo.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PersonalNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Password { get; set; }
    public string Role { get; set; } = UserRole.Customer.ToString();
}
