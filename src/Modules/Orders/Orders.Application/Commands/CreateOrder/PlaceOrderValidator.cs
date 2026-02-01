using FluentValidation;

namespace Orders.Application.Commands.CreateOrder;
internal sealed class PlaceOrderValidator : AbstractValidator<PlaceOrderCommand>
{
    public PlaceOrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}
