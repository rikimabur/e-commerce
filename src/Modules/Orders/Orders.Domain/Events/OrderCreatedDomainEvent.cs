using BuildingBlocks.Domain;

namespace Orders.Domain.Events;
public sealed record OrderCreatedDomainEvent(Guid OrderId, decimal Amount, string PaymentProvider) : DomainEvent;
