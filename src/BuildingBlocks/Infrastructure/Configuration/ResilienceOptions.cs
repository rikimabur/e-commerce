namespace BuildingBlocks.Infrastructure.Configuration;
public sealed class ResilienceOptions
{
    public int RetryCount { get; init; } = 3;
    public int CircuitBreakerFailures { get; init; } = 5;
}