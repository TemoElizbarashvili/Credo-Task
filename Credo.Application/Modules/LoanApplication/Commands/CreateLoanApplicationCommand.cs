using MediatR;

namespace Credo.Application.Modules.LoanApplication.Commands;

public class CreateLoanApplicationCommand : IRequest
{
    public required Domain.Entities.LoanApplication LoanApplication { get; set; } 
}
