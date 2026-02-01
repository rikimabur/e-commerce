using BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.Persistence;

namespace Payments.Infrastructure.Messaging;
public interface IInboxRepository
{
    /// <summary>
    /// Checks whether a message with the given MessageId as Id
    /// was already processed by the given consumer.
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Persists a processed message marker.
    /// Must be idempotent (unique constraint at DB level).
    /// </summary>
    Task AddAsync(OutboxMessage message, CancellationToken cancellationToken);
}

public sealed class InboxRepository(PaymentsDbContext paymentsDbContext) : IInboxRepository
{

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
    {
        return await paymentsDbContext.InboxMessages.AnyAsync(x => x.Id == id);
    }

    public async Task AddAsync(
        OutboxMessage message,
        CancellationToken ct)
    {
        await paymentsDbContext.InboxMessages.AddAsync(message, ct);
        await paymentsDbContext.SaveChangesAsync(ct);
    }
}