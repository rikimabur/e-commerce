namespace Payments.Contracts.Commands;
public record ChargePaymentCommand(
    Guid OrderId,
    decimal Amount,
    string Currency,
    string PaymentProvider
);