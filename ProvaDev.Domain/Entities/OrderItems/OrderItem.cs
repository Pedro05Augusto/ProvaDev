using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Entities.Common;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Entities.Products;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Entities.OrderItems;

public partial class OrderItem : Entity
{
    public int ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    
    public int OrderId { get; private set; }
    
    public Order? Order { get; private set; }
    public Product? Product { get; private set; }

    private OrderItem() { }

    internal OrderItem(Product product, int quantity, int orderId)
    {
        var model = new OrderItemModel
        {
            Name = product.Name,
            UnitPrice = product.Price,
            Quantity = quantity
        };
        
        Guard.Enforce(ValidateNewOrderItem(model));
        
        ProductId = product.Id;
        Name = model.Name;
        UnitPrice = model.UnitPrice;
        Quantity = model.Quantity;
        CreatedAt = DateTime.UtcNow;
        OrderId = orderId;
    }

    public decimal GetTotalPrice() => UnitPrice * Quantity;
}