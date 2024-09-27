using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniInventoryManagementSystem.Domain.Entities;

namespace MiniInventoryManagementSystem.Infra.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Tb_Person");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
            builder.Property(p => p.FederalId).HasColumnName("FederalId").HasMaxLength(11);
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(100);

            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasDefaultValue();

            builder.HasMany(p => p.Orders).WithOne(o => o.Costumer).HasForeignKey(o => o.CostumerId);
        }
    }
}
