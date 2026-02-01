namespace BuildingBlocks.Infrastructure.Configuration;

public sealed class RabbitMqOptions
{
    public string Host { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}