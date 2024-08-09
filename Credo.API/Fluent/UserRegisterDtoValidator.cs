using Credo.API.Modules.Auth.Models;
using FluentValidation;

namespace Credo.API.Fluent;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.PersonalNumber)
            .NotEmpty().WithMessage("Personal Number is required!")
            .Length(11).WithMessage("Personal number must contain 11 digit")
            .Matches(@"^\d+$").WithMessage("Personal number must contain only numbers.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Birth date is required.")
            .LessThan(DateTime.Now).WithMessage("Birth date can not be more than present");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be contain at least 8 character");
    }
}
