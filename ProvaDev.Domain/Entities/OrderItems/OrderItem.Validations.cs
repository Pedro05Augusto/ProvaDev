using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Common.Validators.ValidationExtensions;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Entities.OrderItems;

public partial class OrderItem
{   
    private const int NameMaxLength = 100;
    
    private List<Error> ValidateName(string name)
    {
        return new List<Error>()
            .NotNullOrWhiteSpace(name, nameof(Name))
            .MaximumLength(name, NameMaxLength, nameof(Name));
    }
    
    private List<Error> ValidateUnitPrice(decimal unitPrice)
    {
        return new List<Error>()
            .NotZeroOrNegative(unitPrice, nameof(UnitPrice));
    }

    private List<Error> ValidateQuantity(int quantity)
    {
        return new List<Error>()
            .NotZeroOrNegative(quantity, nameof(Quantity));
    }

    private List<Error> ValidateRemoveQuantity(int quantity)
    {
        var newQuantity = Quantity - quantity;
        return new List<Error>()
            .MustBeTrue(newQuantity > 0, nameof(Quantity), "A quantidade não pode ser menor ou igual a zero após a remoção.");
    }
    
    internal List<Error> ValidateNewOrderItem(OrderItemModel model)
    {
        return new List<Error>()
            .Join(
                ValidateName(model.Name),
                ValidateUnitPrice(model.UnitPrice),
                ValidateQuantity(model.Quantity)
            );
    }
}