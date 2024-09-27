
namespace MiniInventoryManagementSystem.Domain.Entities
{
    public class BranchOffice
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
