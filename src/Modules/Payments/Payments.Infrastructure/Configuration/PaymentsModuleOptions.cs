using BuildingBlocks.Infrastructure.Configuration;

namespace Payments.Infrastructure.Configuration;
public sealed class PaymentsModuleOptions
{
    public const string SectionName = "Payments";

    public DatabaseOptions Database { get; init; } = new();
    public RabbitMqOptions RabbitMq { get; init; } = new();
    public ResilienceOptions Resilience { get; init; } = new();
}

