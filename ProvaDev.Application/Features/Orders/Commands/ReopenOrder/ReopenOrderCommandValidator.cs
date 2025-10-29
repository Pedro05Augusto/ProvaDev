using FluentValidation;

namespace ProvaDev.Application.Features.Orders.Commands.ReopenOrder;

public class ReopenOrderCommandValidator : AbstractValidator<ReopenOrderCommand>
{
    public ReopenOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId deve ser maior que zero");
    }
}
