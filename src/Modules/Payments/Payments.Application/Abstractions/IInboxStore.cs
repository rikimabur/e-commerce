namespace Payments.Application.Abstractions;
public interface IInboxStore
{
    Task<bool> IsDuplicateAsync(Guid messageId, string consumer, CancellationToken ct);
    Task MarkProcessedAsync(Guid messageId, string consumer, CancellationToken ct);
}