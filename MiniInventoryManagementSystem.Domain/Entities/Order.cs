namespace MiniInventoryManagementSystem.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required bool IsCancelled { get; set; }
        public int CostumerId { get; set; }
        public required Person Costumer { get; set; }
        public int BranchOfficeId { get; set; }
        public required BranchOffice BranchOffice { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }
}
