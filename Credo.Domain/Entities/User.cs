using System.ComponentModel.DataAnnotations;
using Credo.Domain.ValueObjects;

namespace Credo.Domain.Entities;

public class User
{
    public int Id { get; set; }
    [MinLength(3), MaxLength(20)]
    public required string UserName { get; set; }
    [MaxLength(20)]
    public required string FirstName { get; set; }
    [MaxLength(30)]
    public required string LastName { get; set; }
    [StringLength(11)]
    public required string PersonalNumber { get; set; }
    public required DateTime DateOfBirth { get; set; }
    [MinLength(8), MaxLength(150)]
    public required string Password { get; set; }
    public string Role { get; set; } = UserRole.Customer.ToString();
    public bool IsDeleted { get; set; } = false;
}
