using BuildingBlocks.Infrastructure.Outbox;
using BuildingBlocks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Payments.Infrastructure.Persistence;
public sealed class PaymentsDbContext : DbContextBase
{
    public DbSet<OutboxMessage> InboxMessages => Set<OutboxMessage>();
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PaymentsDbContext).Assembly);

        modelBuilder.Entity<OutboxMessage>().ToTable("payments_outbox_messages");
    }
}
