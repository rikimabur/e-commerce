using Payments.Application.Models;

namespace Payments.Application.Gateways;
public interface IZaloPayGateway
{
    Task<ZaloPayPaymentResult> PayAsync(ZaloPayPaymentRequest request, CancellationToken ct);
}