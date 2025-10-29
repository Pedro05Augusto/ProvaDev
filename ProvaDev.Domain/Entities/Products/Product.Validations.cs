using ProvaDev.Domain.Common.Validators;
using ProvaDev.Domain.Common.Validators.ValidationExtensions;
using ProvaDev.Domain.Models;

namespace ProvaDev.Domain.Entities.Products;

public partial class Product
{
    private const int NameMaxLength = 100;
    
    private List<Error> ValidateName(string name)
    {
        return new List<Error>()
            .NotNullOrWhiteSpace(name, nameof(Name))
            .MaximumLength(name, NameMaxLength, nameof(Name));
    }

    private List<Error> ValidatePrice(decimal price)
    {
        return new List<Error>()
            .NotZeroOrNegative(price, nameof(Price));
    }

    internal List<Error> ValidateNewProduct(ProductModel model)
    {
        return new List<Error>()
            .Join(
                ValidateName(model.Name),
                ValidatePrice(model.Price)
            );
    }

    internal List<Error> ValidateUpdateProduct(ProductModel model)
    {
        return ValidateNewProduct(model);
    }
}