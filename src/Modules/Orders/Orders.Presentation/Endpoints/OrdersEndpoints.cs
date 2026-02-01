using BuildingBlocks.Application.EndPoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Queries.GetOrderById;
namespace Orders.Presentation.Endpoints;
public class OrdersEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders");

        group.MapPost("/", PlaceOrderAsync)
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithSummary("Place a new order")
            .WithDescription("Creates a new order");

        group.MapGet("/{id:int}", GetOrderByIdAsync)
            .Produces<GetOrderByIdResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithSummary("Get Order By Id")
            .WithDescription("Get order detail");
    }
    private static async Task<IResult> PlaceOrderAsync([FromServices] PlaceOrderHandler placeOrderHandler,
        [FromBody] PlaceOrderCommand request,
        CancellationToken cancellationToken)
    {
        await placeOrderHandler.Handle(request, cancellationToken);
        return Results.Created();
    }

    private static async Task<IResult> GetOrderByIdAsync([FromServices] GetOrderByIdQueryHandler getOrderByIdQueryHandler,
        [FromRoute] GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var result = await getOrderByIdQueryHandler.Handle(request, cancellationToken);
        return Results.Ok(result);
    }
}


