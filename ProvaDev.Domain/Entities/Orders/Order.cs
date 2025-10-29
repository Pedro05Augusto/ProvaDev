using ProvaDev.Domain.Entities.Common;
using ProvaDev.Domain.Entities.OrderItems;
using ProvaDev.Domain.Entities.Customers;

namespace ProvaDev.Domain.Entities.Orders;

public partial class Order : Entity, IAggregateRoot
{
    public int CustomerId { get; private set; }
    public bool IsClosed { get; private set; }
    public decimal TotalAmount { get; private set; }
    public Customer? Customer { get; private set; }
    
    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    private Order()
    {
    }
    
    private Order(int customerId)
    {
        CustomerId = customerId;
        IsClosed = false;
        TotalAmount = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public static Order Create(int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("CustomerId deve ser maior que zero.", nameof(customerId));
        
        return new Order(customerId);
    }
}