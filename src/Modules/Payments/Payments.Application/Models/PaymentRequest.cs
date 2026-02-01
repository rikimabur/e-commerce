namespace Payments.Application.Models;
public sealed record PaymentRequest(
    Guid OrderId,
    decimal Amount,
    string Currency
);
