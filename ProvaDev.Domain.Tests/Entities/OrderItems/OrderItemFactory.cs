using ProvaDev.Domain.Entities.OrderItems;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Entities.Products;

namespace ProvaDev.Domain.Tests.Entities.OrderItems;

public class OrderItemFactory
{
    public static OrderItem Create(Product product, int quantity = 1)
    {
        var order = Order.Create(1);
        order.AddProduct(product, quantity);
        return order.Items.First();
    }
}