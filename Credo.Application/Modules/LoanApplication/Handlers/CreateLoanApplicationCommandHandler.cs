using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Domain.Services;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class CreateLoanApplicationCommandHandler : LoanApplicationBaseRequestHandler, IRequestHandler<CreateLoanApplicationCommand>
{
    public CreateLoanApplicationCommandHandler(LoanApplicationsService loanApplicationsService) : base(loanApplicationsService) { }

    public async Task Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        await _loanApplicationsService.SubmitLoanApplicationAsync(request.LoanApplication);
    }
}
