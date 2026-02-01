using BuildingBlocks.Infrastructure.Repositories;
using Orders.Domain.Aggregates;

namespace Orders.Domain.Repositories;
public interface IOrderRepository : IRepository<Order>
{
}