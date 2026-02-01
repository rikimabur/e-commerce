using MassTransit;
using MediatR;
using Orders.Contracts.Events;
using Orders.Domain.Events;

namespace Orders.Application.EventHandlers;
public sealed class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderCreatedDomainEventHandler(
        IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken ct)
    {
        var integrationEvent = new OrderCreatedIntegrationEvent(
            notification.OrderId,
            DateTime.UtcNow,
            notification.PaymentProvider,
            notification.Amount
            );

        await _publishEndpoint.Publish(integrationEvent, ct);
    }
}