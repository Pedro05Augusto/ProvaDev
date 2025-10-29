using ProvaDev.Infrastructure.Data.Context;
using ProvaDev.Domain.Entities.Customers;
using ProvaDev.Domain.Entities.Products;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Models.Customers;
using ProvaDev.Domain.Models;

namespace ProvaDev.WebApi.Seed;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Customers.Any() || db.Products.Any() || db.Orders.Any())
            return;

        var customers = new[]
        {
            new Customer(new CustomerModel { Name = "Alice Silva", Email = "alice@exemplo.com", Telephone = "11999998888" }),
            new Customer(new CustomerModel { Name = "Bruno Souza", Email = "bruno@exemplo.com", Telephone = "21988887777" })
        };
        db.Customers.AddRange(customers);

        var products = new[]
        {
            new Product(new ProductModel { Name = "Notebook", Price = 4500m }),
            new Product(new ProductModel { Name = "Mouse", Price = 80m }),
            new Product(new ProductModel { Name = "Teclado", Price = 150m })
        };
        db.Products.AddRange(products);

        db.SaveChanges();

        var order1 = Order.Create(customers[0].Id);
        order1.AddProduct(products[0], 1);
        order1.AddProduct(products[1], 2);

        var order2 = Order.Create(customers[1].Id);
        order2.AddProduct(products[2], 1);
        order2.Close();

        db.Orders.AddRange(order1, order2);
        db.SaveChanges();
    }
}
