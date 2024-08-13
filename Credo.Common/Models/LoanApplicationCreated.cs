namespace Credo.Common.Models;

public class LoanApplicationCreated
{
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public int Period { get; set; } 
    public int UserId { get; set; }
    public LoanType LoanType { get; set; }
    public LoanStatus Status { get; set; }
}
