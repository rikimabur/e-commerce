using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Infrastructure.UnitOfWork;
using Orders.Domain.Aggregates;
using Orders.Domain.Repositories;

namespace Orders.Application.Commands.CreateOrder;

public class PlaceOrderHandler : ICommandHandler<PlaceOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _uow;

    public PlaceOrderHandler(IOrderRepository orderRepository, IUnitOfWork uow)
    {
        _orderRepository = orderRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(PlaceOrderCommand cmd, CancellationToken ct)
    {
        var order = Order.Place(cmd.CustomerId, cmd.Amount, cmd.PaymentProvider);
        await _orderRepository.AddAsync(order, ct);
        await _uow.SaveChangesAsync(ct);
        return order.Id;
    }
}