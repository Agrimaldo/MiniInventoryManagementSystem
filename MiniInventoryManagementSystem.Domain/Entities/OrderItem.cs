
namespace MiniInventoryManagementSystem.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public required string ProductCode { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public required decimal UnitPrice { get; set; }
        public required decimal Total { get; set; }
        public required bool IsCancelled { get; set; }

        public int OrderId { get; set; }
        public required Order Order { get; set; }
    }
}
