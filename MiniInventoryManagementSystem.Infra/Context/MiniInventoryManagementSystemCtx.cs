using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.Domain.Entities;
using MiniInventoryManagementSystem.Infra.Mapping;

namespace MiniInventoryManagementSystem.Infra.Context
{
    public  class MiniInventoryManagementSystemCtx : DbContext
    {
        public MiniInventoryManagementSystemCtx(DbContextOptions<MiniInventoryManagementSystemCtx> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new BranchOfficeMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<BranchOffice> branchOffices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
