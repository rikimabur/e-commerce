namespace BuildingBlocks.Infrastructure.Outbox;
/// <summary>
/// The Idempotent Consumer Pattern
/// </summary>
public interface IInboxStore
{
    Task<bool> ExistsAsync(Guid messageId, CancellationToken ct);
    Task SaveAsync(OutboxMessage message, CancellationToken ct);
}
