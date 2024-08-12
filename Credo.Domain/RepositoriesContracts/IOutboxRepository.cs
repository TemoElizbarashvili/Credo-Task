using Credo.Domain.Entities;

namespace Credo.Domain.RepositoriesContracts;

public interface IOutboxRepository
{
    void Add(OutboxMessage message);
    Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken);
    Task MarkAsProcessedAsync(Guid messageId, CancellationToken cancellationToken);
}
