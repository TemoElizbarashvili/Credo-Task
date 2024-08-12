using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure.Repositories;

public class OutboxRepository : IOutboxRepository
{
    private readonly ApplicationDbContext _context;

    public OutboxRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(OutboxMessage message)
        => _context.OutboxMessages.Add(message);

    public async Task<IEnumerable<OutboxMessage>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken)
        => await _context.OutboxMessages
            .Where(m => !m.ProcessedOn.HasValue)
            .ToListAsync(cancellationToken);


    public async Task MarkAsProcessedAsync(Guid messageId, CancellationToken cancellationToken)
    {
        var message = await _context.OutboxMessages.FindAsync(messageId);
        if (message is not null)
        {
            message.ProcessedOn = DateTime.Now;
            _context.OutboxMessages.Update(message);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
