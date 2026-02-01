namespace Orders.Contracts.Events;
/// <summary>
/// Integration event published when a new order is created.
/// </summary>
public sealed record OrderCreatedIntegrationEvent(
    Guid OrderId,
    DateTime OccurredAt,
    string PaymentProvider,
    decimal Amount
);