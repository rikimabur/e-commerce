namespace Payments.Infrastructure.Payments.ZaloPay;
public sealed class ZaloPayOptions
{
    public string AppId { get; init; } = default!;
    public string Key1 { get; init; } = default!;
    public string Endpoint { get; init; } = default!;
    public int TimeoutSeconds { get; init; } = 10;
}