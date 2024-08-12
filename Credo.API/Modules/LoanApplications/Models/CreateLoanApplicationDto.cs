using Microsoft.OpenApi.Attributes;

namespace Credo.API.Modules.LoanApplications.Models;

public record CreateLoanApplicationDto(
    LoanTypeDto LoanType,
    decimal Amount,
    Currency Currency,
    int Period);

public enum LoanTypeDto
{
    [Display("Fast")]
    Fast = 0,

    [Display("Auto")]
    Auto = 1,

    [Display("Installment")]
    Installment = 2
}


public enum Currency
{
    [Display("GEL")]
    GEL = 0,

    [Display("USD")]
    USD = 1,

    [Display("EUR")]
    EUR = 2
}