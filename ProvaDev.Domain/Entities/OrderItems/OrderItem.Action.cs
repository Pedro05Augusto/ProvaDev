using ProvaDev.Domain.Common.Validators;

namespace ProvaDev.Domain.Entities.OrderItems;

public partial class OrderItem
{
    internal void AddQuantity(int quantity)
    {
        Guard.Enforce(ValidateQuantity(quantity));
        Quantity += quantity;
        
        TouchUpdated();
    }

    internal void RemoveQuantity(int quantity)
    {
        Guard.Enforce(ValidateQuantity(quantity));
        Guard.Enforce(ValidateRemoveQuantity(quantity));
        
        Quantity -= quantity;
        
        TouchUpdated();
    }

    internal void UpdateQuantity(int newQuantity)
    {
        Guard.Enforce(ValidateQuantity(newQuantity));
        Quantity = newQuantity;
        
        TouchUpdated();
    }
}