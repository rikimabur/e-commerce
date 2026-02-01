namespace Payments.Contracts.Events;
public record PaymentChargedIntegrationEvent(
    Guid OrderId,
    decimal Amount,
    string Currency);