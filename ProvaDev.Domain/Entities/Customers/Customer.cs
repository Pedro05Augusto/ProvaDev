using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Entities.Common;
using ProvaDev.Domain.Models.Customers;
using System.Collections.Generic;

namespace ProvaDev.Domain.Entities.Customers;

public partial class Customer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Telephone { get; private set; }

    public ICollection<Orders.Order> Orders { get; private set; } = new List<Orders.Order>();

    private Customer()
    {
    }
    
    public Customer(CustomerModel model)
    {
        Guard.Enforce(ValidateNewCustomer(model));
        
        Name = model.Name;
        Email = model.Email;
        Telephone = model.Telephone;
        CreatedAt = DateTime.UtcNow;
    }
}