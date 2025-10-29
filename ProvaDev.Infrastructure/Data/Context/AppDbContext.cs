using Microsoft.EntityFrameworkCore;
using ProvaDev.Domain.Entities.Customers;
using ProvaDev.Domain.Entities.OrderItems;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Entities.Products;

namespace ProvaDev.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}