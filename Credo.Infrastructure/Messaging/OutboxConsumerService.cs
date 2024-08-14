using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Credo.Infrastructure.EventProcessing;
using Microsoft.Extensions.Options;

namespace Credo.Infrastructure.Messaging;

public class OutboxConsumerService : BackgroundService
{
    private readonly ILogger<OutboxConsumerService> _logger;
    private readonly IEventProcessor _eventProcessor;
    private readonly RabbitMqConfiguration _config;
    private IConnection? _connection;
    private IModel? _channel;

    public OutboxConsumerService(
        ILogger<OutboxConsumerService> logger,
        IOptions<RabbitMqConfiguration> configOptions,
        IEventProcessor eventProcessor)
    {
        _logger = logger;
        _config = configOptions.Value;
        _eventProcessor = eventProcessor;
        InitializeRabbitMQ();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                _logger.LogInformation("Consumed Message - {@Message}", message);
                await _eventProcessor.ProcessEvent(message);
            };

            _channel.BasicConsume(queue: _config.QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory() { HostName = _config.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
}