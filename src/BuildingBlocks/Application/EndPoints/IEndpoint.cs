using Microsoft.AspNetCore.Routing;
namespace BuildingBlocks.Application.EndPoints;
public interface IEndpoint
{
    /// <summary>
    /// Maps category-related endpoints to the application's endpoint route builder.
    /// </summary>
    /// <param name="app">The endpoint route builder to which the endpoints will be mapped.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}
