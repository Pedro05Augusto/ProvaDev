using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Models.Customers;

namespace ProvaDev.Domain.Entities.Customers;

public partial class Customer
{
    public void UpdateCustomer(CustomerModel model)
    {
        Guard.Enforce(ValidateUpdateCustomer(model));
        
        Name = model.Name;
        Email = model.Email;
        Telephone = model.Telephone;
        
        TouchUpdated();
    }
}