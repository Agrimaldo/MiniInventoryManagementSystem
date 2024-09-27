using MiniInventoryManagementSystem.Domain.Dto;

namespace MiniInventoryManagementSystem.Domain.Interfaces
{
    public interface IOrderService
    {
        bool Create(OrderInput input);
        bool Update(OrderInput input);
        bool Cancel(int id);
        bool CancelItem(int itemId);
    }
}
