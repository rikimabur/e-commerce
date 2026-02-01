using MediatR;

namespace BuildingBlocks.Domain;
public abstract record DomainEvent : INotification
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}