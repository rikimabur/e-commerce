using BuildingBlocks.Domain;
using Orders.Domain.Enums;
using Orders.Domain.Events;

namespace Orders.Domain.Aggregates;
public class Order : AggregateRoot
{
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal Amount { get; private set; }
    public string PaymentProvider { get; private set; }
    private Order() { }

    public static Order Place(Guid customerId, decimal amount, string paymentProvider)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Amount = amount,
            PaymentProvider = paymentProvider,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Created
        };
        order.AddDomainEvent(new OrderCreatedDomainEvent(order.Id, order.Amount, order.PaymentProvider));
        return order;
    }
    public void MarkPaid()
    {
        if (Status != OrderStatus.Created)
            return;

        Status = OrderStatus.Paid;
    }

    public void MarkPaymentFailed()
    {
        if (Status != OrderStatus.Created)
            return;

        Status = OrderStatus.PaymentFailed;
    }
}
