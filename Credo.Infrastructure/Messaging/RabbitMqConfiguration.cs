namespace Credo.Infrastructure.Messaging;

public class RabbitMqConfiguration
{
    public required string HostName { get; set; }
    public required string Port { get; set; }
    public required string QueueName { get; set; }
    public required string ExchangeName { get; set; }
    public required string RoutingKey { get; set; }
}