using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Entities.Products;

public partial class Product
{
    public void UpdateProduct(ProductModel model)
    {
        Guard.Enforce(ValidateUpdateProduct(model));
        
        Name = model.Name;
        Price = model.Price;
        
        TouchUpdated();
    }
}