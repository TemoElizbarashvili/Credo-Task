using Microsoft.OpenApi.Attributes;

namespace Credo.API.Modules.LoanApplications.Models;

public record LoanApplicationQueryDto(
int Id,
int UserId,
LoanTypeDto LoanType,
decimal Amount,
string Currency,
int Period,
LoanStatusDto Status,
UserForApplicationQueryDto? User);

public enum LoanStatusDto
{
    [Display("InProgress")]
    InProgress,

    [Display("Sent")]
    Sent,

    [Display("Submitted")]
    Submitted,

    [Display("Declined")]
    Declined
}