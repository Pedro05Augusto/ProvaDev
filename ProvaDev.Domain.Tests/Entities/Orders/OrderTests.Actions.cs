using FluentAssertions;
using ProvaDev.Domain.Exceptions;
using ProvaDev.Domain.Tests.Entities.Products;

namespace ProvaDev.Domain.Tests.Entities.Orders;

public partial class OrderTests
{
    [Test]
    public void AddProduct_ValidData_Success()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid("Laptop", 1000.00m);

        order.AddProduct(product, 2);

        order.Items.Should().HaveCount(1);
        order.Items.First().ProductId.Should().Be(product.Id);
        order.Items.First().Name.Should().Be("Laptop");
        order.Items.First().UnitPrice.Should().Be(1000.00m);
        order.Items.First().Quantity.Should().Be(2);
        order.TotalAmount.Should().Be(2000.00m);
        order.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void AddProduct_SameProductTwice_IncrementsQuantity()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid("Mouse", 50.00m);

        order.AddProduct(product, 1);
        order.AddProduct(product, 2);

        order.Items.Should().HaveCount(1);
        order.Items.First().Quantity.Should().Be(3);
        order.TotalAmount.Should().Be(150.00m);
    }

    [Test]
    public void AddProduct_MultipleProducts_CalculatesTotalCorrectly()
    {
        var order = OrderFactory.CreateValid();
        var product1 = ProductFactory.CreateValid("Laptop", 1000.00m);
        var product2 = ProductFactory.CreateValid("Mouse", 50.00m);

        order.AddProduct(product1, 1);
        order.AddProduct(product2, 2);

        order.Items.Should().HaveCount(2);
        order.TotalAmount.Should().Be(1100.00m);
    }

    [Test]
    public void AddProduct_NullProduct_ThrowsException()
    {
        var order = OrderFactory.CreateValid();

        var act = () => order.AddProduct(null!, 1);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*product*");
    }

    [Test]
    public void AddProduct_ClosedOrder_ThrowsException()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);
        order.Close();

        var act = () => order.AddProduct(product, 1);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*fechado*");
    }

    [Test]
    public void AddProduct_InvalidQuantity_ThrowsException()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();

        var act = () => order.AddProduct(product, 0);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*Quantity*");
    }

    [Test]
    public void RemoveProduct_ValidProduct_Success()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid("Laptop", 1000.00m);
        order.AddProduct(product, 1);

        order.RemoveProduct(product.Id);

        order.Items.Should().BeEmpty();
        order.TotalAmount.Should().Be(0);
        order.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void RemoveProduct_NonExistent_ThrowsException()
    {
        var order = OrderFactory.CreateValid();

        var act = () => order.RemoveProduct(999);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*não encontrado*");
    }

    [Test]
    public void RemoveProduct_ClosedOrder_ThrowsException()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);
        order.Close();

        var act = () => order.RemoveProduct(product.Id);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*fechado*");
    }

    [Test]
    public void UpdateProductQuantity_ValidData_Success()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid("Laptop", 1000.00m);
        order.AddProduct(product, 2);

        order.UpdateProductQuantity(product.Id, 5);

        order.Items.First().Quantity.Should().Be(5);
        order.TotalAmount.Should().Be(5000.00m);
        order.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void UpdateProductQuantity_NonExistent_ThrowsException()
    {
        var order = OrderFactory.CreateValid();

        var act = () => order.UpdateProductQuantity(999, 5);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*não encontrado*");
    }

    [Test]
    public void UpdateProductQuantity_ClosedOrder_ThrowsException()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);
        order.Close();

        var act = () => order.UpdateProductQuantity(product.Id, 5);

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*fechado*");
    }

    [Test]
    public void CloseOrder_WithProducts_Success()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);

        order.Close();

        order.IsClosed.Should().BeTrue();
        order.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void CloseOrder_Empty_ThrowsException()
    {
        var order = OrderFactory.CreateValid();

        var act = () => order.Close();

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*sem itens*");
    }

    [Test]
    public void CloseOrder_AlreadyClosed_ThrowsException()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);
        order.Close();

        var act = () => order.Close();

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*já está fechado*");
    }

    [Test]
    public void ReopenOrder_WhenClosed_Success()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);
        order.Close();

        order.Reopen();

        order.IsClosed.Should().BeFalse();
        order.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void ReopenOrder_AlreadyOpen_ThrowsException()
    {
        var order = OrderFactory.CreateValid();

        var act = () => order.Reopen();

        act.Should().Throw<DomainValidationException>()
            .WithMessage("*já está aberto*");
    }

    [Test]
    public void Items_ReturnsCollection()
    {
        var order = OrderFactory.CreateValid();
        var product = ProductFactory.CreateValid();
        order.AddProduct(product, 1);

        var items = order.Items;

        items.Should().HaveCount(1);
        items.Should().BeAssignableTo<ICollection<Domain.Entities.OrderItems.OrderItem>>();
    }

    [Test]
    public void TotalAmount_AfterOperations_RemainsConsistent()
    {
        var order = OrderFactory.CreateValid();
        var product1 = ProductFactory.CreateValid("Laptop", 1000.00m);
        var product2 = ProductFactory.CreateValid("Mouse", 50.00m);
        var product3 = ProductFactory.CreateValid("Keyboard", 100.00m);

        order.AddProduct(product1, 1);
        order.AddProduct(product2, 2);
        order.AddProduct(product3, 1);
        order.TotalAmount.Should().Be(1200.00m);

        order.RemoveProduct(product2.Id);
        order.TotalAmount.Should().Be(1100.00m);

        order.UpdateProductQuantity(product1.Id, 2);
        order.TotalAmount.Should().Be(2100.00m);
    }
}