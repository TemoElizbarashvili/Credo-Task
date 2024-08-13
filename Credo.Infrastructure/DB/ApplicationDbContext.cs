using Credo.Common.Models;
using Credo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Credo.Infrastructure.DB;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.PersonalNumber)
            .IsUnique();

        modelBuilder.Entity<LoanApplication>()
            .Property(l => l.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<LoanApplication>()
            .Property(l => l.Status)
            .HasConversion(new EnumToStringConverter<LoanStatus>());

        modelBuilder.Entity<LoanApplication>()
            .Property(l => l.LoanType)
            .HasConversion(new EnumToStringConverter<LoanType>());
    }
}
