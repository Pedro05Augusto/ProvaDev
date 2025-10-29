using FluentValidation;

namespace ProvaDev.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido")
            .MaximumLength(254).WithMessage("Email deve ter no máximo 254 caracteres");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .MaximumLength(20).WithMessage("Telefone deve ter no máximo 20 caracteres")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter apenas números (10 ou 11 dígitos)");
    }
}
