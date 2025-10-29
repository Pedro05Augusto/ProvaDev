using FluentAssertions;
using ProvaDev.Domain.Exceptions;
using ProvaDev.Domain.Models.Customers;

namespace ProvaDev.Domain.Tests.Entities.Customers;

public partial class CustomerTests
{
    [Test]
    public void UpdateCustomer_ValidData_Success()
    {
        var customer = CustomerFactory.CreateValid("John Doe", "john@test.com", "11987654321");
        var updateModel = new CustomerModel
        {
            Name = "Jane Doe",
            Email = "jane@test.com",
            Telephone = "21987654321"
        };

        customer.UpdateCustomer(updateModel);

        customer.Name.Should().Be("Jane Doe");
        customer.Email.Should().Be("jane@test.com");
        customer.Telephone.Should().Be("21987654321");
        customer.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Test]
    public void UpdateCustomer_NullName_ThrowsException()
    {
        var customer = CustomerFactory.CreateValid();
        var updateModel = new CustomerModel
        {
            Name = null!,
            Email = "john@test.com",
            Telephone = "11987654321"
        };

        var act = () => customer.UpdateCustomer(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void UpdateCustomer_NullEmail_ThrowsException()
    {
        var customer = CustomerFactory.CreateValid();
        var updateModel = new CustomerModel
        {
            Name = "John Doe",
            Email = null!,
            Telephone = "11987654321"
        };

        var act = () => customer.UpdateCustomer(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Email*");
    }

    [Test]
    public void UpdateCustomer_NullTelephone_ThrowsException()
    {
        var customer = CustomerFactory.CreateValid();
        var updateModel = new CustomerModel
        {
            Name = "John Doe",
            Email = "john@test.com",
            Telephone = null!
        };

        var act = () => customer.UpdateCustomer(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Telephone*");
    }
}