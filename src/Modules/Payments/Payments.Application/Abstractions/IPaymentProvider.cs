using Payments.Application.Models;

namespace Payments.Application.Abstractions;
public interface IPaymentProvider
{
    string Name { get; }

    Task<PaymentResult> ChargeAsync(PaymentRequest request, CancellationToken ct);
}