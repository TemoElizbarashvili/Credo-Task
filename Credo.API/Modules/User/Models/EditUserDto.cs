namespace Credo.API.Modules.User.Models;

public record EditUserDto(
    int Id,
    string UserName,
    string FirstName,
    string LastName,
    string PersonalNumber,
    DateTime DateOfBirth
);
