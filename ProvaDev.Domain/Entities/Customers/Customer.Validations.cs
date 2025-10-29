using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Common.Validators.ValidationExtensions;
using ProvaDev.Domain.Models.Customers;

namespace ProvaDev.Domain.Entities.Customers;

public partial class Customer
{
    private const int NameMaxLength = 100;
    private const int EmailMaxLength = 254;
    private const int TelephoneMaxLength = 20;
    
    private List<Error> ValidateName(string name)
    {
        return new List<Error>()
            .NotNullOrWhiteSpace(name, nameof(Name))
            .MaximumLength(name, NameMaxLength, nameof(Name));
    }
    
    private List<Error> ValidateEmail(string email)
    {
        return new List<Error>()
            .NotNullOrWhiteSpace(email, nameof(Email))
            .MaximumLength(email, EmailMaxLength, nameof(Email))
            .ValidEmail(email, nameof(Email));
    }
    
    private List<Error> ValidateTelephone(string telephone)
    {
        return new List<Error>()
            .NotNullOrWhiteSpace(telephone, nameof(Telephone))
            .MaximumLength(telephone, TelephoneMaxLength, nameof(Telephone))
            .ValidTelephone(telephone, nameof(Telephone));
    }

    internal List<Error> ValidateNewCustomer(CustomerModel model)
    {
        return new List<Error>()
            .Join(
                ValidateName(model.Name),
                ValidateEmail(model.Email),
                ValidateTelephone(model.Telephone)
                );

    }

    internal List<Error> ValidateUpdateCustomer(CustomerModel model)
    {
        return ValidateNewCustomer(model);
    }
}