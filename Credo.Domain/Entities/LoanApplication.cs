using Credo.Domain.ValueObjects;

namespace Credo.Domain.Entities;

public class LoanApplication
{
    public int Id { get; set; }
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    required public string Currency { get; set; }
    public int Period { get; set; } // Period in Months
    public LoanStatus LoanStatus { get; set; }
}
