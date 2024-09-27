
namespace MiniInventoryManagementSystem.Domain.Dto
{
    public class OrderInput
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal Total { get; set; }
        public OrderPersonInput? Costumer { get; set; }
        public OrderBranchOfficeInput? BranchOffice { get; set; }
        public IList<OrderItemInput>? Items { get; set; }
    }

    public class OrderPersonInput 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FederalId { get; set; }
        public required string Email { get; set; }
    }
    public class OrderBranchOfficeInput
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
    public class OrderItemInput 
    { 
        public int Id { get; set; }
        public string? ProductCode { get; set; } = "";
        public string? ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

    }
}
