using Credo.Application.Modules.LoanApplication.Queries;
using Credo.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class GetApplicationByIdQueryHandler : LoanApplicationBaseRequestHandler, IRequestHandler<GetApplicationByIdQuery, Domain.Entities.LoanApplication?>
{
    public GetApplicationByIdQueryHandler(
        LoanApplicationsService service,
        ILogger<LoanApplicationBaseRequestHandler> logger) : base(service, logger) { }

    public async Task<Domain.Entities.LoanApplication?> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        => await LoanApplicationsService.GetByIdAsync(request.Id, request.WithUser);
}
