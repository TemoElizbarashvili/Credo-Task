namespace Credo.API.Modules.Auth.Models;

public record UserLoginDto(
    string UserName,
    string Password);