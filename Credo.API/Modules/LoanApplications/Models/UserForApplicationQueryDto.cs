namespace Credo.API.Modules.LoanApplications.Models;

public record UserForApplicationQueryDto(
    string UserName,
    string FirstName,
    string LastName,
    string PersonalNumber);