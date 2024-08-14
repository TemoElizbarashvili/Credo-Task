using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.Services;
using Credo.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using Credo.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace Credo.Infrastructure.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EventProcessor> _logger;
    private readonly RabbitMqConfiguration _config;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<EventProcessor> logger, IOptions<RabbitMqConfiguration> config)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _config = config.Value;
    }

    public async Task ProcessEvent(string message)
    {
        await AddApplicationAsync(message);
    }

    private async Task AddApplicationAsync(string applicationPublishedMessage)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var loanApplicationsService = scope.ServiceProvider.GetRequiredService<LoanApplicationsService>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var applicationCreated = JsonSerializer.Deserialize<LoanApplicationCreated>(applicationPublishedMessage);

            try
            {
                if (applicationCreated != null)
                {
                    var application = new LoanApplication
                    {
                        User = null,
                        Currency = applicationCreated!.Currency,
                        Amount = applicationCreated.Amount,
                        UserId = applicationCreated.UserId,
                        Status = LoanStatus.Sent,
                        LoanType = applicationCreated.LoanType,
                        Period = applicationCreated.Period
                    };

                    await loanApplicationsService.CreateLoanApplicationAsync(application);
                    await unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while consuming and adding loan application in Database, Exception: {@Exception}", ex);
            }
        }
    }

    public void Publish<T>(T message)
    {
        var factory = new ConnectionFactory() { HostName = _config.HostName };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: _config.ExchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queue: _config.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: _config.QueueName, exchange: _config.ExchangeName, routingKey: _config.RoutingKey);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: _config.ExchangeName,
            routingKey: _config.RoutingKey,
            body: body,
            basicProperties: null);
    }
}