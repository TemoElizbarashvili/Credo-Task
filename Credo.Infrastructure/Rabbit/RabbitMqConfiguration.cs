namespace Credo.Infrastructure.Rabbit;

public class RabbitMqConfiguration
{
    required public string HostName { get; set; }
    required public string QueueName { get; set; }
    required public string ExchangeName { get; set; }
    required public string RoutingKey { get; set; }
}
