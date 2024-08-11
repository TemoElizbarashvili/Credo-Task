using Credo.API.Modules.User.Models;
using Credo.Domain.RepositoriesContracts;
using FluentValidation;

namespace Credo.API.Modules.User.Validators;

public class EditUserDtoValidator : AbstractValidator<EditUserDto>
{
    public EditUserDtoValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserName)
            .MustAsync(async (userName, _) => await userRepository.IsUserNameUniqueAsync(userName))
            .WithMessage("User name must be unique.")
            .NotEmpty().WithMessage("User name must be provided.")
            .MinimumLength(3).WithMessage("User name must contain at least 3 character.");

        RuleFor(x => x.PersonalNumber)
            .NotEmpty().WithMessage("Personal Number is required!")
            .Length(11).WithMessage("Personal number must contain 11 digit")
            .Matches(@"^\d+$").WithMessage("Personal number must contain only numbers.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Birth date is required.")
            .LessThan(DateTime.Now).WithMessage("Birth date can not be more than present");
    }
}
