using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Exceptions;
using Orders.Application.Mappings;
using Orders.Domain.Repositories;

namespace Orders.Application.Queries.GetOrderById;
public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Order {request.Id} not found");
        var response = order.ToGetOrderByIdResponse();
        return response;
    }
}
