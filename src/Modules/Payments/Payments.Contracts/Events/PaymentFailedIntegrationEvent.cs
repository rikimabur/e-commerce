namespace Payments.Contracts.Events;
public record PaymentFailedIntegrationEvent(
    Guid OrderId,
    string Reason);