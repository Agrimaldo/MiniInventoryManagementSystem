using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniInventoryManagementSystem.Domain.Entities;

namespace MiniInventoryManagementSystem.Infra.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Tb_Order");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(p => p.Number).HasColumnName("Number");
            builder.Property(p => p.Total).HasColumnName("Total");
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasDefaultValue();
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt");

            builder.Property(p => p.IsCancelled).HasColumnName("IsCancelled");

            builder.Property(p => p.CostumerId).HasColumnName("CostumerId");
            builder.Property(p => p.BranchOfficeId).HasColumnName("BranchOfficeId");

            builder.HasOne(p => p.Costumer).WithMany(r => r.Orders).HasForeignKey(i => i.CostumerId);
            builder.HasOne(p => p.BranchOffice).WithMany(r => r.Orders).HasForeignKey(i => i.BranchOfficeId);
        }
    }
}
