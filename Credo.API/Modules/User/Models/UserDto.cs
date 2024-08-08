namespace Credo.API.Modules.User.Models;

public record UserDto(
    int Id,
    string UserName,
    string FirstName,
    string LastName,
    string PersonalNumber,
    DateTime DateOfBirth,
    string Role);
