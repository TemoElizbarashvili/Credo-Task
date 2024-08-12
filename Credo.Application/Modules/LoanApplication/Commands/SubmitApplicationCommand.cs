using MediatR;

namespace Credo.Application.Modules.LoanApplication.Commands;

public class SubmitApplicationCommand : IRequest
{
    public required int Id { get; set; }
}