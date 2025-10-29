using FluentValidation;

namespace ProvaDev.Application.Features.Orders.Commands.RemoveProductFromOrder;

public class RemoveProductFromOrderCommandValidator : AbstractValidator<RemoveProductFromOrderCommand>
{
    public RemoveProductFromOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId deve ser maior que zero");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId deve ser maior que zero");
    }
}
