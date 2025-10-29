using ProvaDev.Domain.Entities.Products;
using ProvaDev.Domain.Models;
using System.Reflection;

namespace ProvaDev.Domain.Tests.Entities.Products;

public static class ProductFactory
{
    private static int _idCounter = 1;

    public static Product CreateValid(string name = "Product Test", decimal price = 10.00m)
    {
        var model = new ProductModel
        {
            Name = name,
            Price = price
        };
        var product = new Product(model);
        
        SetId(product, _idCounter++);
        
        return product;
    }

    public static ProductModel CreateValidModel(string name = "Product Test", decimal price = 10.00m)
    {
        return new ProductModel
        {
            Name = name,
            Price = price
        };
    }

    private static void SetId(Product product, int id)
    {
        var idProperty = typeof(Product).BaseType!.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
        idProperty!.SetValue(product, id);
    }
}