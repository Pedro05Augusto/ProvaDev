using FluentValidation;

namespace ProvaDev.Application.Features.Orders.Commands.CloseOrder;

public class CloseOrderCommandValidator : AbstractValidator<CloseOrderCommand>
{
    public CloseOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId deve ser maior que zero");
    }
}
