using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniInventoryManagementSystem.Domain.Entities;

namespace MiniInventoryManagementSystem.Infra.Mapping
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("Tb_OrderItem");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();


            builder.Property(p => p.ProductCode).HasColumnName("ProductCode").HasMaxLength(10);
            builder.Property(p => p.ProductName).HasColumnName("ProductName").HasMaxLength(100);

            builder.Property(p => p.Quantity).HasColumnName("Quantity");
            builder.Property(p => p.Discount).HasColumnName("Discount");
            builder.Property(p => p.UnitPrice).HasColumnName("UnitPrice");

            builder.Property(p => p.Total).HasColumnName("Total");
            builder.Property(p => p.IsCancelled).HasColumnName("IsCancelled");

            builder.Property(p => p.OrderId).HasColumnName("OrderId");

            builder.HasOne(p => p.Order).WithMany(r => r.Items).HasForeignKey(i => i.OrderId);
        }
    }
}
