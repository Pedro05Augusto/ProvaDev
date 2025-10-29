using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaDev.Domain.Entities.Orders;

namespace ProvaDev.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.CustomerId)
            .IsRequired();
        
        builder.Property(o => o.IsClosed)
            .IsRequired();
        
        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(o => o.CreatedAt)
            .IsRequired();
        
        builder.Property(o => o.UpdatedAt);
        
        builder.HasMany(o => o.Items)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => o.IsClosed);
        builder.HasIndex(o => o.CreatedAt);
    }
}