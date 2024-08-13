using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.Services;
using Credo.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Credo.Infrastructure.Messaging;

public class OutboxConsumerService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMessageQueueService _messageQueueService;
    private readonly ILogger<OutboxConsumerService> _logger;

    public OutboxConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        IMessageQueueService messageQueueService,
        ILogger<OutboxConsumerService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _messageQueueService = messageQueueService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    var loanApplicationService = scope.ServiceProvider.GetRequiredService<LoanApplicationsService>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var messages = new List<LoanApplicationCreated>();

                    _messageQueueService.Consume<LoanApplicationCreated>(message =>
                        messages.Add(message));

                    var loanApplications = messages.Select(message => new LoanApplication
                    {
                        Currency = message.Currency,
                        User = null,
                        Amount = message.Amount,
                        LoanType = message.LoanType,
                        Period = message.Period,
                        Status = LoanStatus.Sent,
                        UserId = message.UserId
                    });

                    await loanApplicationService.AddRangeAsync(loanApplications);
                    await unitOfWork.CompleteAsync(stoppingToken);

                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to consume messages. Exception {@Ex}", ex);
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(25), stoppingToken);
        }
    }
}