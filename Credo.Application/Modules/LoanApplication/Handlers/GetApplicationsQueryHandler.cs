using Credo.Application.Modules.LoanApplication.Models;
using Credo.Application.Modules.LoanApplication.Queries;
using Credo.Domain.Services;
using Credo.Infrastructure.Messaging;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class GetApplicationsQueryHandler : LoanApplicationBaseRequestHandler, IRequestHandler<GetApplicationsQuery, List<LoanApplicationCreated>>
{
    private readonly IMessageQueueService _messageQueueService;

    public GetApplicationsQueryHandler(IMessageQueueService messageQueueService, LoanApplicationsService loanApplicationsService) : base(loanApplicationsService)
    {
        _messageQueueService = messageQueueService;
    }

    public Task<List<LoanApplicationCreated>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        var messages = new List<LoanApplicationCreated>();

        _messageQueueService.Consume<LoanApplicationCreated>((message, ea) =>
        {
            messages.Add(message);
        });

        return Task.FromResult(messages);
    }
}
