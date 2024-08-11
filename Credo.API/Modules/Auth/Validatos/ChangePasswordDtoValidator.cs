using Credo.API.Modules.Auth.Models;
using FluentValidation;

namespace Credo.API.Modules.Auth.Validatos;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull().WithMessage("User name is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull().WithMessage("Password field is required.")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters");
    }
}
