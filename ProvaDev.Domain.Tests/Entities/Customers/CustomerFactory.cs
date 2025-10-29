using ProvaDev.Domain.Entities.Customers;
using ProvaDev.Domain.Models.Customers;

namespace ProvaDev.Domain.Tests.Entities.Customers;

public static class CustomerFactory
{
    public static Customer CreateValid(string name = "John Doe", string email = "john@test.com", string telephone = "11987654321")
    {
        var model = new CustomerModel
        {
            Name = name,
            Email = email,
            Telephone = telephone
        };
        return new Customer(model);
    }

    public static CustomerModel CreateValidModel(string name = "John Doe", string email = "john@test.com", string telephone = "11987654321")
    {
        return new CustomerModel
        {
            Name = name,
            Email = email,
            Telephone = telephone
        };
    }
}