using FluentValidation;

namespace ProvaDev.Application.Features.Orders.Commands.UpdateProductQuantity;

public class UpdateProductQuantityCommandValidator : AbstractValidator<UpdateProductQuantityCommand>
{
    public UpdateProductQuantityCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId deve ser maior que zero");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId deve ser maior que zero");

        RuleFor(x => x.NewQuantity)
            .GreaterThan(0).WithMessage("Nova quantidade deve ser maior que zero");
    }
}
