using MiniInventoryManagementSystem.Domain.Utilities;
using RabbitMQ.Client.Events;

namespace MiniInventoryManagementSystem.Domain.Interfaces
{
    public interface IMessageBus
    {
        void Publish<T>(MessageType messageType, T content);
        T Consume<T>(MessageType messageType, EventHandler<BasicDeliverEventArgs> receiverMethod);
    }
}
