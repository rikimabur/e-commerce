namespace BuildingBlocks.Domain;
public abstract class AggregateRoot: Entity
{
    private readonly List<object> _events = [];

    public IReadOnlyCollection<object> DomainEvents => _events;

    protected void AddDomainEvent(object @event)
        => _events.Add(@event);

    public void ClearEvents() => _events.Clear();
}