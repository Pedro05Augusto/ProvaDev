namespace ProvaDev.Domain.Models;

public class OrderItemModel
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}