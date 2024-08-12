using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Application.Modules.LoanApplication.Models;
using Credo.Domain.Services;
using Credo.Domain.ValueObjects;
using Credo.Infrastructure.Messaging;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class SubmitApplicationCommandHandler : LoanApplicationBaseRequestHandler, IRequestHandler<SubmitApplicationCommand>
{
    private readonly IMessageQueueService _messageQueueService;

    public SubmitApplicationCommandHandler(IMessageQueueService messageQueueService, LoanApplicationsService loanApplicationsService) : base(loanApplicationsService)
    {
        _messageQueueService = messageQueueService;
    }

    public async Task Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
    {
        var founded = false;
        _messageQueueService.Consume<LoanApplicationCreated>( (message, ea) =>
        {
            if (request.Id == message.Id)
            {
                founded = true;
                _messageQueueService.Ack(ea.DeliveryTag);
            }
        });
        if (founded)
        {
            await _loanApplicationsService.ProcessLoanApplicationAsync(request.Id, LoanStatus.Submitted);
        }
    }
}
