using Credo.Common.Models;
using Credo.Domain.ValueObjects;

namespace Credo.API.Modules.LoanApplications.Models;

public record LoanApplicationListItemDto(
    int Id,
    LoanType LoanType,
    decimal Amount,
    string Currency,
    int Period,
    int UserId,
    LoanStatus Status,
    ApplicationAuthorDetailsDto AuthorDetails);

public record ApplicationAuthorDetailsDto(
    string? AuthorFirstName,
    string? AuthorLastName,
    string? AuthorPersonalNumber);
