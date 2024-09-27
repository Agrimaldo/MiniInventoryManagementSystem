using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniInventoryManagementSystem.Domain.Entities;

namespace MiniInventoryManagementSystem.Infra.Mapping
{
    public class BranchOfficeMap : IEntityTypeConfiguration<BranchOffice>
    {
        public void Configure(EntityTypeBuilder<BranchOffice> builder)
        {
            builder.ToTable("Tb_BranchOffice");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasDefaultValue();
            builder.HasMany(p => p.Orders).WithOne(o => o.BranchOffice).HasForeignKey(o => o.BranchOfficeId);
        }
    }
}
