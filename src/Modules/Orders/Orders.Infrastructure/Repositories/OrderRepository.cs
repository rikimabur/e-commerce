using BuildingBlocks.Infrastructure.Repositories;
using Orders.Domain.Aggregates;
using Orders.Domain.Repositories;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.Repositories;
public sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrdersDbContext context)
        : base(context)
    {
    }
}