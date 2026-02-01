using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Infrastructure.Persistence;
using System.Text.Json;

namespace Orders.Infrastructure.Outbox;
public sealed class OutboxDispatcher : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPublishEndpoint _publishEndpoint;
    public OutboxDispatcher(
        IServiceScopeFactory scopeFactory,
        IPublishEndpoint publishEndpoint)
    {
        _scopeFactory = scopeFactory;
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

            var messages = await db.OutboxMessages
                .Where(x => x.ProcessedOn == null)
                .OrderBy(x => x.OccurredOn)
                .Take(20)
                .ToListAsync(ct);

            foreach (var msg in messages)
            {
                try
                {
                    var type = Type.GetType(msg.Type)!;
                    var evt = JsonSerializer.Deserialize(msg.Payload, type)!;

                    await _publishEndpoint.Publish(evt, ct);

                    msg.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    msg.RetryCount++;
                    msg.Error = ex.Message;
                }
            }

            await db.SaveChangesAsync(ct);
            await Task.Delay(1000, ct);
        }
    }
}