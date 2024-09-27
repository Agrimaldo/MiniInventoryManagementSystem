using MiniInventoryManagementSystem.Domain.Dto;
using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Domain.Utilities;
using Serilog;
using System.Text.Json;

namespace MiniInventoryManagementSystem.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger _logger;
        private readonly IMessageBus _messageBus;
        public OrderService(ILogger logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }
        public bool Cancel(int id)
        {
            _logger.Information(JsonSerializer.Serialize(id));
            try
            {
                _logger.Information(JsonSerializer.Serialize(new OrderInput() { Id = id }));
                _messageBus.Publish<OrderInput>(MessageType.Cancel, new OrderInput() { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"input : {JsonSerializer.Serialize(id)} \r\n Msg: {ex.Message} ");
                return false;
            }
        }

        public bool CancelItem(int itemId)
        {
            _logger.Information(JsonSerializer.Serialize(itemId));
            try
            {
                _logger.Information(JsonSerializer.Serialize(new OrderItemInput() { Id = itemId }));
                _messageBus.Publish<OrderItemInput>(MessageType.CancelItem, new OrderItemInput() { Id = itemId });
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"input : {JsonSerializer.Serialize(itemId)} \r\n Msg: {ex.Message} ");
                return false;
            }
        }

        public bool Create(OrderInput input)
        {
            try
            {
                _logger.Information(JsonSerializer.Serialize(input));
                _messageBus.Publish<OrderInput>(MessageType.Create, input);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"input : {JsonSerializer.Serialize(input)} \r\n Msg: {ex.Message} ");
                return false;
            }
        }

        public bool Update(OrderInput input)
        {
            try
            {
                _logger.Information(JsonSerializer.Serialize(input));
                _messageBus.Publish<OrderInput>(MessageType.Update, input);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"input : {JsonSerializer.Serialize(input)} \r\n Msg: {ex.Message} ");
                return false;
            }
        }
    }
}
