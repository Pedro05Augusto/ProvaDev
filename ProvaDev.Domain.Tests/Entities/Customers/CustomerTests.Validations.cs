using FluentAssertions;
using ProvaDev.Domain.Exceptions;
using ProvaDev.Domain.Models.Customers;
using NUnit.Framework;

namespace ProvaDev.Domain.Tests.Entities.Customers;

public partial class CustomerTests
{
    [Test]
    public void CreateCustomer_WithValidData_Success()
    {
        var model = CustomerFactory.CreateValidModel("John Doe", "john@test.com", "11987654321");

        var customer = new Domain.Entities.Customers.Customer(model);

        customer.Name.Should().Be("John Doe");
        customer.Email.Should().Be("john@test.com");
        customer.Telephone.Should().Be("11987654321");
        customer.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Test]
    public void CreateCustomer_NullName_ThrowsException()
    {
        var model = new CustomerModel
        {
            Name = null!,
            Email = "john@test.com",
            Telephone = "11987654321"
        };

        var act = () => new Domain.Entities.Customers.Customer(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateCustomer_EmptyName_ThrowsException()
    {
        var model = new CustomerModel
        {
            Name = string.Empty,
            Email = "john@test.com",
            Telephone = "11987654321"
        };

        var act = () => new Domain.Entities.Customers.Customer(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateCustomer_NullEmail_ThrowsException()
    {
        var model = new CustomerModel
        {
            Name = "John Doe",
            Email = null!,
            Telephone = "11987654321"
        };

        var act = () => new Domain.Entities.Customers.Customer(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Email*");
    }

    [Test]
    public void CreateCustomer_NullTelephone_ThrowsException()
    {
        var model = new CustomerModel
        {
            Name = "John Doe",
            Email = "john@test.com",
            Telephone = null!
        };

        var act = () => new Domain.Entities.Customers.Customer(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Telephone*");
    }
}