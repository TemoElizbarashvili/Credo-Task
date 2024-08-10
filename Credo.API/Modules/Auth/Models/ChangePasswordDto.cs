namespace Credo.API.Modules.Auth.Models;

public record ChangePasswordDto(
    string UserName,
    string Password);
