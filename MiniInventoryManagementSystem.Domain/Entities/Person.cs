
namespace MiniInventoryManagementSystem.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FederalId { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}