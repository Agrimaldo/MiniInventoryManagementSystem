using Testcontainers.RabbitMq;
using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Domain.Services;
using RabbitMQ.Client;
using Serilog;
using MiniInventoryManagementSystem.Domain.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Channels;
using Docker.DotNet.Models;
using Bogus;

namespace MiniInventoryManagementSystem.Test.Domain.Services
{
    public class OrderServiceTest : IAsyncLifetime
    {
        private readonly RabbitMqContainer _rabbitMqContainer;
        public OrderServiceTest() {

            _rabbitMqContainer = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithName("rabbitmq-test-container")
            .WithPortBinding(5672, 5672)
            .WithPortBinding(15672, 15672)
            .WithUsername("guest").WithPassword("guest")
            .Build();
        }

        [Fact]
        public void CreateWithSucess() 
        {
            var messageBus = new MessageBus(new ConnectionFactory { HostName = _rabbitMqContainer.Hostname, Port = _rabbitMqContainer.GetMappedPublicPort(5672) });
            IOrderService orderService = new OrderService(Log.ForContext<OrderService>(), messageBus);

            var input = new Faker<OrderInput>();

            Assert.True(orderService.Create(input));
        }


        [Fact]
        public void UpdateWithSucess()
        {
            var messageBus = new MessageBus(new ConnectionFactory { HostName = _rabbitMqContainer.Hostname, Port = _rabbitMqContainer.GetMappedPublicPort(5672) });
            IOrderService orderService = new OrderService(Log.ForContext<OrderService>(), messageBus);

            var input = new Faker<OrderInput>();

            Assert.True(orderService.Update(input));
        }

        [Fact]
        public void CancelWithSucess()
        {
            var messageBus = new MessageBus(new ConnectionFactory { HostName = _rabbitMqContainer.Hostname, Port = _rabbitMqContainer.GetMappedPublicPort(5672) });
            IOrderService orderService = new OrderService(Log.ForContext<OrderService>(), messageBus);

            Assert.True(orderService.Cancel(1));
        }

        [Fact]
        public void CancelItemWithSucess()
        {
            var messageBus = new MessageBus(new ConnectionFactory { HostName = _rabbitMqContainer.Hostname, Port = _rabbitMqContainer.GetMappedPublicPort(5672) });
            IOrderService orderService = new OrderService(Log.ForContext<OrderService>(), messageBus);

            Assert.True(orderService.CancelItem(1));
        }

        public async Task DisposeAsync()
        {
            await _rabbitMqContainer.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await _rabbitMqContainer.StartAsync();
        }
    }
}
