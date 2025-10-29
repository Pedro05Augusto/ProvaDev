using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Entities.OrderItems;
using ProvaDev.Domain.Entities.Products;

namespace ProvaDev.Domain.Entities.Orders;

public partial class Order
{
    public void AddProduct(Product product, int quantity)
    {
        Guard.Enforce(ValidateCanAddProduct(product));

        var existingItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
        
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            var newItem = new OrderItem(product, quantity, Id);
            Items.Add(newItem);
        }

        RecalculateTotalAmount();
        TouchUpdated();
    }

    public void RemoveProduct(int productId)
    {
        Guard.Enforce(ValidateCanRemoveProduct(productId));

        var item = Items.First(i => i.ProductId == productId);
        Items.Remove(item);
        RecalculateTotalAmount();
        TouchUpdated();
    }

    public void UpdateProductQuantity(int productId, int newQuantity)
    {
        Guard.Enforce(ValidateCanUpdateQuantity(productId, newQuantity));

        var item = Items.First(i => i.ProductId == productId);
        item.UpdateQuantity(newQuantity);
        RecalculateTotalAmount();
        TouchUpdated();
    }

    public void Close()
    {
        Guard.Enforce(ValidateCanClose());

        IsClosed = true;
        TouchUpdated();
        
    }

    public void Reopen()
    {
        Guard.Enforce(ValidateCanReopen());

        IsClosed = false;
        TouchUpdated();
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.GetTotalPrice());
    }
}