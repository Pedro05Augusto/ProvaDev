using FluentAssertions;
using ProvaDev.Domain.Entities.Orders;

namespace ProvaDev.Domain.Tests.Entities.Orders;

public partial class OrderTests
{
    [Test]
    public void CreateOrder_ValidCustomerId_Success()
    {
        var order = Order.Create(1);

        order.CustomerId.Should().Be(1);
        order.IsClosed.Should().BeFalse();
        order.TotalAmount.Should().Be(0);
        order.Items.Should().BeEmpty();
        order.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void CreateOrder_ZeroCustomerId_ThrowsException()
    {
        var act = () => Order.Create(0);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CustomerId*");
    }

    [Test]
    public void CreateOrder_NegativeCustomerId_ThrowsException()
    {
        var act = () => Order.Create(-1);

        act.Should().Throw<ArgumentException>()
            .WithMessage("*CustomerId*");
    }
}