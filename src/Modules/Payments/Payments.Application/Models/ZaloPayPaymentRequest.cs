namespace Payments.Application.Models;
public sealed class ZaloPayPaymentRequest
{
    public string OrderId { get; init; } = default!;

    public long Amount { get; init; }

    public string Description { get; init; } = default!;

    public string CallbackUrl { get; init; } = default!;
}