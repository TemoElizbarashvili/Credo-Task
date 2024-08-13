using System.Text.Json;
using Credo.Common.Models;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Domain.ValueObjects;
using Credo.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Credo.Infrastructure.Messaging;

public class OutboxPublisherService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMessageQueueService _messageQueueService;
    private readonly ILogger<OutboxPublisherService> _logger;

    public OutboxPublisherService(
        IServiceScopeFactory serviceScopeFactory,
        IMessageQueueService messageQueueService,
        ILogger<OutboxPublisherService> logger)
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
                var outboxRepository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                var loanApplicationService = scope.ServiceProvider.GetRequiredService<LoanApplicationsService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var unprocessedMessages = await outboxRepository.GetUnprocessedMessagesAsync(stoppingToken);

                foreach (var message in unprocessedMessages)
                {
                    try
                    {
                        var messageType = Type.GetType(message.Type);
                        if (messageType == null)
                        {
                            _logger.LogError("Type '{@MessageType}' could not be found for message ID {@MessageId}", message.Type, message.Id);
                            continue;
                        }
                        var deserializedMessage = JsonSerializer.Deserialize(message.Data, messageType!);
                        if (deserializedMessage == null)
                        {
                            _logger.LogError("Failed to deserialize message data for message ID {@MessageId}", message.Id);
                            continue;
                        }
                        var loanApplicationId = (int)messageType.GetProperty("Id")?.GetValue(deserializedMessage)!;
                        await loanApplicationService.ProcessLoanApplicationAsync(loanApplicationId, LoanStatus.Sent);

                        _messageQueueService.Publish(deserializedMessage);

                        await outboxRepository.MarkAsProcessedAsync(message.Id, stoppingToken);
                        await unitOfWork.SaveChangesAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Failed to process outbox message with ID {@MessageId}, Exception {@Ex}", message.Id, ex);
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
    }
}