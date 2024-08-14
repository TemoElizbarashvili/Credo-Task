using MediatR;

namespace Credo.Application.Modules.LoanApplication.Commands;

public class DeclineApplicationCommand : IRequest
{
    public required int Id { get; set; }
}