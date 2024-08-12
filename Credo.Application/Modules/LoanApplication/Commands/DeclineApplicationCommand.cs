using Credo.Domain.Entities;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Commands;

public class DeclineApplicationCommand : IRequest
{
    public required OutboxMessage Application { get; set; }
    public required int ApplicationId { get; set; }
}