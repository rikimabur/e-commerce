using BuildingBlocks.Infrastructure.Configuration;

namespace Orders.Infrastructure.Configuration;
public sealed class OrdersModuleOptions
{
    public const string SectionName = "Orders";

    public DatabaseOptions Database { get; init; } = new();
    public RabbitMqOptions RabbitMq { get; init; } = new();
    public ResilienceOptions Resilience { get; init; } = new();
}
