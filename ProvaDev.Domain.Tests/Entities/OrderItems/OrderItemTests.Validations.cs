using FluentAssertions;
using ProvaDev.Domain.Tests.Entities.Products;

namespace ProvaDev.Domain.Tests.Entities.OrderItems;

public partial class OrderItemTests
{
    [Test]
    public void GetTotalPrice_CalculatesCorrectly()
    {
        var product = ProductFactory.CreateValid("Laptop", 1000.00m);
        var orderItem = OrderItemFactory.Create(product, 3);

        var total = orderItem.GetTotalPrice();

        total.Should().Be(3000.00m);
    }

    [Test]
    public void GetTotalPrice_WithSingleQuantity_ReturnsUnitPrice()
    {
        var product = ProductFactory.CreateValid("Mouse", 50.00m);
        var orderItem = OrderItemFactory.Create(product, 1);

        var total = orderItem.GetTotalPrice();

        total.Should().Be(50.00m);
    }
}