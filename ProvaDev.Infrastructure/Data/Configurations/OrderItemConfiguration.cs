using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProvaDev.Domain.Entities.OrderItems;

namespace ProvaDev.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        
        builder.Property(oi => oi.ProductId)
            .IsRequired();
        
        builder.Property(oi => oi.OrderId)
            .IsRequired();
        
        builder.Property(oi => oi.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);
        
        builder.Property(oi => oi.Quantity)
            .IsRequired();
        
        builder.Property(oi => oi.CreatedAt)
            .IsRequired();
        
        builder.Property(oi => oi.UpdatedAt);
        
        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(oi => oi.OrderId);
        builder.HasIndex(oi => oi.ProductId);
    }
}