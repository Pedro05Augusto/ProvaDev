using FluentAssertions;
using ProvaDev.Domain.Exceptions;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Tests.Entities.Products;

public partial class ProductTests
{
    [Test]
    public void CreateProduct_WithValidData_Success()
    {
        var model = ProductFactory.CreateValidModel("Laptop", 1500.00m);

        var product = new Domain.Entities.Products.Product(model);

        product.Name.Should().Be("Laptop");
        product.Price.Should().Be(1500.00m);
        product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void CreateProduct_NullName_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = null!,
            Price = 10.00m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateProduct_EmptyName_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = string.Empty,
            Price = 10.00m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateProduct_WhitespaceName_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = "   ",
            Price = 10.00m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateProduct_NameTooLong_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = new string('A', 101),
            Price = 10.00m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Name*");
    }

    [Test]
    public void CreateProduct_ZeroPrice_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = "Product",
            Price = 0m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Price*");
    }

    [Test]
    public void CreateProduct_NegativePrice_ThrowsException()
    {
        var model = new ProductModel
        {
            Name = "Product",
            Price = -10.00m
        };

        var act = () => new Domain.Entities.Products.Product(model);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Price*");
    }
}