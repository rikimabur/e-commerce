using MassTransit;

namespace Payments.Application.Sagas;
public sealed class PaymentSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = default!;
    public bool OrderCreatedHandled { get; set; }
    public Guid OrderId { get; set; }
}
