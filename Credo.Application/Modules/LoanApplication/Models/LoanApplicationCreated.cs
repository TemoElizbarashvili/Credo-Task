using Credo.Domain.ValueObjects;

namespace Credo.Application.Modules.LoanApplication.Models;

public class LoanApplicationCreated
{
    public int Id { get; set; }
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public int Period { get; set; } 
    public int UserId { get; set; }
    public LoanStatus Status { get; set; }
}
