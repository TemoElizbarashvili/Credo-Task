using Credo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.DB;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
