using BuildingBlocks.Application.CQRS;

namespace Orders.Application.Commands.CreateOrder;
public record PlaceOrderCommand(Guid CustomerId, decimal Amount, string PaymentProvider) : ICommand<Guid>;

