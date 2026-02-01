using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Outbox;
using BuildingBlocks.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BuildingBlocks.Infrastructure.Persistence;
public abstract class DbContextBase : DbContext, IUnitOfWork
{
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected DbContextBase(DbContextOptions options) : base(options) { }

    public override async Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        AddOutboxMessages();
        return await base.SaveChangesAsync(ct);
    }

    private void AddOutboxMessages()
    {
        var aggregates = ChangeTracker
                        .Entries<AggregateRoot>()
                        .Where(e => e.Entity.DomainEvents.Any());

        foreach (var entry in aggregates)
        {
            foreach (var domainEvent in entry.Entity.DomainEvents)
            {
                OutboxMessages.Add(new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().Name,
                    Payload = JsonSerializer.Serialize(domainEvent),
                    OccurredOn = DateTime.UtcNow
                });
            }

            entry.Entity.ClearEvents();
        }
    }
}