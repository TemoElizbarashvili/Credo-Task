using Credo.Application.Modules.LoanApplication.Queries;
using Credo.Common.Models;
using Credo.Domain.Services;
using Credo.Infrastructure.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class GetApplicationsQueryHandler : LoanApplicationBaseRequestHandler, IRequestHandler<GetApplicationsQuery, PagedList<Domain.Entities.LoanApplication>?>
{
    private readonly IMessageQueueService _messageQueueService;

    public GetApplicationsQueryHandler(
        IMessageQueueService messageQueueService,
        LoanApplicationsService loanApplicationsService,
        ILogger<LoanApplicationBaseRequestHandler> logger) : base(loanApplicationsService, logger)
    {
        _messageQueueService = messageQueueService;
    }

    public async Task<PagedList<Domain.Entities.LoanApplication>?> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var applications = await _loanApplicationsService.ApplicationsPagedListAsync(request.Query, true);
            return applications;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error occurred while retrieving loan applications in handler, Exception - {@Exception}", ex);
        }

        return null;
    }
}
