using Credo.Domain.ValueObjects;

namespace Credo.Domain.Entities;

public class LoanApplication
{
    public int Id { get; set; }
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public int Period { get; set; } // Period in Months
    public LoanStatus Status { get; set; }

    //Relations

    public int UserId { get; set; }
    public required User? User { get; set; }

    public void UpdateStatus(LoanStatus newStatus)
    {
        Status = newStatus;
    }
}
