using FluentValidation;

namespace ProvaDev.Application.Features.Orders.Commands.AddProductToOrder;

public class AddProductToOrderCommandValidator : AbstractValidator<AddProductToOrderCommand>
{
    public AddProductToOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId deve ser maior que zero");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId deve ser maior que zero");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero");
    }
}
