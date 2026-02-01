using Orders.Application.Queries.GetOrderById;
using Orders.Domain.Aggregates;

namespace Orders.Application.Mappings
{
    public static class OrderMappings
    {
        public static GetOrderByIdResponse ToGetOrderByIdResponse(this Order orders)
        {
            return new GetOrderByIdResponse(orders.Id, orders.CustomerId);
        }
    }
}
