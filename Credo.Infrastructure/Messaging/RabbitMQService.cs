using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Credo.Infrastructure.Messaging;

public interface IMessageQueueService
{
    void Publish<T>(T message);
}

public class RabbitMQService : IMessageQueueService
{
    private readonly RabbitMqConfiguration _config;

    public RabbitMQService(RabbitMqConfiguration config)
    {
        _config = config;
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
