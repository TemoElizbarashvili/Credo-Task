namespace Credo.Infrastructure.EventProcessing;

public interface IEventProcessor
{
    Task ProcessEvent(string message);
    void Publish<T>(T message);
}
