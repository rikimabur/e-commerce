using Payments.Application.Abstractions;
using Payments.Application.Models;

namespace Payments.Application.Providers;
public class StripePaymentProvider : IPaymentProvider
{
    public string Name => "Stripe";

    public async Task<PaymentResult> ChargeAsync(PaymentRequest request, CancellationToken ct)
    {
        await Task.Delay(300, ct);

        return new PaymentResult(
            Success: true,
            Provider: Name,
            TransactionId: $"stripe_{Guid.NewGuid()}",
            ErrorMessage: null);
    }
}