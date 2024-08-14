using Credo.Domain.Entities;
using Credo.Domain.ValueObjects;

namespace Credo.Infrastructure.DB;

public class SeedData
{
    private readonly ApplicationDbContext _context;

    public SeedData(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedManager()
    {
        var password = BCrypt.Net.BCrypt.HashPassword("admin");
        var user = new User
        {
            DateOfBirth = DateTime.Now.AddYears(-18),
            FirstName = "Admin",
            LastName = "Admin",
            Password = password,
            PersonalNumber = "00000000000",
            UserName = "admin",
            IsDeleted = false,
            Role = UserRole.Manager.ToString(),
        };
        if (_context.Users.Any()) return;
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
