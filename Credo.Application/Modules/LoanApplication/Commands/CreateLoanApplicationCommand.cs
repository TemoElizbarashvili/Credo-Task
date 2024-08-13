using Credo.Common.Models;
using Credo.Domain.ValueObjects;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Commands;

public class CreateLoanApplicationCommand : IRequest
{
    public LoanType LoanType { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public int Period { get; set; } 
    public int UserId { get; set; }
    public LoanStatus Status { get; set; }
}
