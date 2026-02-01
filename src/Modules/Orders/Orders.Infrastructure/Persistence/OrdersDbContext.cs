using BuildingBlocks.Infrastructure.Outbox;
using BuildingBlocks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Orders.Domain.Aggregates;

namespace Orders.Infrastructure.Persistence;
public sealed class OrdersDbContext : DbContextBase
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(OrdersDbContext).Assembly);

        modelBuilder.Entity<OutboxMessage>().ToTable("order_outbox_messages");
    }
}
