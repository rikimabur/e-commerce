using BuildingBlocks.Domain;
using Payments.Domain.Enums;

namespace Payments.Domain.Aggregates;
public class Payment : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public PaymentStatus Status { get; private set; }

    public static Payment Create(Guid orderId)
        => new()
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Status = PaymentStatus.Pending
        };

    public void Succeed() => Status = PaymentStatus.Succeeded;
    public void Fail() => Status = PaymentStatus.Failed;
}