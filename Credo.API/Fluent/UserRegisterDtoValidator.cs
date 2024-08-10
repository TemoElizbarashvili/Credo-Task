using System.ComponentModel.DataAnnotations;
using Credo.API.Modules.Auth.Models;
using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;
using FluentValidation;

namespace Credo.API.Fluent;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator(IUserRepository userRepository)
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

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be contain at least 8 character");
    }
}
