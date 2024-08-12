using Credo.Domain.ValueObjects;

namespace Credo.API.Modules.LoanApplications.Models;

public record LoanApplicationMessageDto(
    int Id,
    LoanType LoanType,
    decimal Amount,
    string Currency,
    int Period,
    int UserId,
    LoanStatus Status);
