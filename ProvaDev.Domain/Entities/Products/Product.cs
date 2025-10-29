using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Entities.Common;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Entities.Products;

public partial class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private Product() { }

    public Product(ProductModel model)
    {
        Guard.Enforce(ValidateNewProduct(model));
        
        Name = model.Name;
        Price = model.Price;
        CreatedAt = DateTime.UtcNow;
    }
}