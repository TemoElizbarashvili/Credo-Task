using Credo.Domain.ValueObjects;

namespace Credo.Domain.Entities;

public class User
{
    public int Id { get; set; }
    required public string FirstName { get; set; }
    required public string LastName { get; set; }
    required public string PersonalNumber { get; set; }
    required public DateOnly BornDate { get; set; }
    required public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.Customer;
}
