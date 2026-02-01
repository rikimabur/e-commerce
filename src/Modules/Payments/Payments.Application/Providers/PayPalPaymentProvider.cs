using Payments.Application.Abstractions;
using Payments.Application.Models;

namespace Payments.Application.Providers;
public sealed class PayPalPaymentProvider : IPaymentProvider
{
    public string Name => "PayPal";

    public async Task<PaymentResult> ChargeAsync(
        PaymentRequest request,
        CancellationToken ct)
    {
        await Task.Delay(300, ct);

        return new PaymentResult(
            Success: true,
            Provider: Name,
            TransactionId: $"paypal_{Guid.NewGuid()}",
            ErrorMessage: null);
    }
}