using Credo.Application.Modules.LoanApplication.Queries;
using Credo.Domain.Services;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class GetApplicationByIdQueryHandler : LoanApplicationBaseRequestHandler, IRequestHandler<GetApplicationByIdQuery, Domain.Entities.LoanApplication?>
{
    public GetApplicationByIdQueryHandler(LoanApplicationsService service) : base(service) { }
    public async Task<Domain.Entities.LoanApplication?> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        => await _loanApplicationsService.GetByIdAsync(request.Id, request.WithUser);
}
