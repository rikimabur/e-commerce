using BuildingBlocks.Application.CQRS;

namespace Orders.Application.Queries.GetOrderById;
public record GetOrderByIdRequest(Guid Id) : IQuery<GetOrderByIdResponse>;

