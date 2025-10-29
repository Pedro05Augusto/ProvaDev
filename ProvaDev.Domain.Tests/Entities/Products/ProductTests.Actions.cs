using FluentAssertions;
using ProvaDev.Domain.Exceptions;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Tests.Entities.Products;

public partial class ProductTests
{
    [Test]
    public void UpdateProduct_ValidData_Success()
    {
        var product = ProductFactory.CreateValid("Laptop", 1000.00m);
        var updateModel = new ProductModel
        {
            Name = "Gaming Laptop",
            Price = 1500.00m
        };

        product.UpdateProduct(updateModel);

        product.Name.Should().Be("Gaming Laptop");
        product.Price.Should().Be(1500.00m);
        product.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void UpdateProduct_ZeroPrice_ThrowsException()
    {
        var product = ProductFactory.CreateValid();
        var updateModel = new ProductModel
        {
            Name = "Product",
            Price = 0m
        };

        var act = () => product.UpdateProduct(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Price*");
    }

    [Test]
    public void UpdateProduct_NegativePrice_ThrowsException()
    {
        var product = ProductFactory.CreateValid();
        var updateModel = new ProductModel
        {
            Name = "Product",
            Price = -10.00m
        };

        var act = () => product.UpdateProduct(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Price*");
    }

    [Test]
    public void UpdateProduct_NullName_ThrowsException()
    {
        var product = ProductFactory.CreateValid();
        var updateModel = new ProductModel
        {
            Name = null!,
            Price = 100.00m
        };

        var act = () => product.UpdateProduct(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void UpdateProduct_EmptyName_ThrowsException()
    {
        var product = ProductFactory.CreateValid();
        var updateModel = new ProductModel
        {
            Name = string.Empty,
            Price = 100.00m
        };

        var act = () => product.UpdateProduct(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void UpdateProduct_NameTooLong_ThrowsException()
    {
        var product = ProductFactory.CreateValid();
        var updateModel = new ProductModel
        {
            Name = new string('A', 101),
            Price = 100.00m
        };

        var act = () => product.UpdateProduct(updateModel);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }
}