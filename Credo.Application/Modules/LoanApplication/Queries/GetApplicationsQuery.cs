using Credo.Common.Models;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Queries;

public class GetApplicationsQuery : IRequest<PagedList<Domain.Entities.LoanApplication>?>
{
    public required LoanApplicationQueryParameters Query { get; set; }
}