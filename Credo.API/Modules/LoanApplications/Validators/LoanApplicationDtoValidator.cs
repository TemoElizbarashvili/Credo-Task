using Credo.API.Modules.LoanApplications.Models;
using FluentValidation;

namespace Credo.API.Modules.LoanApplications.Validators;

public class LoanApplicationDtoValidator : AbstractValidator<CreateLoanApplicationDto>
{
    public LoanApplicationDtoValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .NotNull().WithMessage("Amount field is required.")
            .GreaterThanOrEqualTo(100).WithMessage("Amount must be at least 100");

        RuleFor(x => x.Period)
            .NotEmpty()
            .NotNull().WithMessage("Period field is required.")
            .GreaterThanOrEqualTo(2).WithMessage("Period must be at least 2 month");
    }
}
