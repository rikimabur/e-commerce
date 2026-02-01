using BuildingBlocks.Infrastructure.Outbox;

namespace Payments.Infrastructure.Messaging;
public sealed class InboxStore : IInboxStore
{
    private readonly IInboxRepository _repository;

    public InboxStore(IInboxRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExistsAsync(Guid messageId, CancellationToken ct)
    {
        return await _repository.ExistsAsync(messageId, ct);
    }

    public async Task SaveAsync(OutboxMessage message, CancellationToken ct)
    {
        await _repository.AddAsync(message, ct);
    }
}