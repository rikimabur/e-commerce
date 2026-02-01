using MassTransit;
using Orders.Contracts.Events;
using Payments.Contracts.Commands;
using Payments.Contracts.Events;
namespace Payments.Application.Sagas;
public sealed class PaymentSaga : MassTransitStateMachine<PaymentSagaState>
{
    public State Charging { get; private set; } = null!;
    public State Completed { get; private set; } = null!;
    public State Failed { get; private set; } = null!;

    public Event<OrderCreatedIntegrationEvent> OrderCreated { get; private set; } = null!;
    public Event<PaymentChargedIntegrationEvent> PaymentCharged { get; private set; } = null!;
    public Event<PaymentFailedIntegrationEvent> PaymentFailed { get; private set; } = null!;

    public PaymentSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreated,
            x => x.CorrelateById(ctx => ctx.Message.OrderId));

        Event(() => PaymentCharged,
            x => x.CorrelateById(ctx => ctx.Message.OrderId));

        Event(() => PaymentFailed,
            x => x.CorrelateById(ctx => ctx.Message.OrderId));

        Initially(
            When(OrderCreated)
                .Then(ctx =>
                {
                    ctx.Saga.OrderId = ctx.Message.OrderId;
                })
                .Send(new Uri("queue:charge-payment"), ctx =>
                    new ChargePaymentCommand(
                        ctx.Message.OrderId,
                        ctx.Message.Amount,
                        "USD",
                        ctx.Message.PaymentProvider))
                .TransitionTo(Charging)
        );

        During(Charging,
            When(PaymentCharged)
                .TransitionTo(Completed)
                .Finalize(),

            When(PaymentFailed)
                .TransitionTo(Failed)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}