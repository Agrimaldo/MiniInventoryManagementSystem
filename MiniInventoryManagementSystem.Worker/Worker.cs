
using Serilog;
using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Domain.Utilities;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MiniInventoryManagementSystem.Domain.Dto;
using MiniInventoryManagementSystem.Domain.Entities;

namespace MiniInventoryManagementSystem.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        IRepository _repository;
        IMessageBus _messageBus;
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(4);


        public Worker(ILogger<Worker> logger, IRepository repository, IMessageBus messageBus)
        {
            _logger = logger;
            _repository = repository;
            _messageBus = messageBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            foreach (MessageType item in Enum.GetValues(typeof(MessageType)))
            {
                switch (item)
                {
                    case MessageType.Create:
                        _messageBus.Consume<OrderInput>(MessageType.Create, CreateReceiver);
                        break;
                    case MessageType.Update:
                        _messageBus.Consume<OrderInput>(MessageType.Update, UpdateReceiver);
                        break;
                    case MessageType.Cancel:
                        _messageBus.Consume<OrderInput>(MessageType.Cancel, CancelReceiver);
                        break;
                    case MessageType.CancelItem:
                        _messageBus.Consume<OrderItemInput>(MessageType.CancelItem, CancelItemReceiver);
                        break;
                }
            }

            await Task.Delay(1000, stoppingToken);
        }

        public void CreateReceiver(object? sender, BasicDeliverEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Body.ToArray());
            OrderInput input = JsonSerializer.Deserialize<OrderInput>(content)!;

            _logger.LogInformation("Worker running Create at: {time}", DateTimeOffset.Now);
            _logger.LogInformation($"CreateTask {JsonSerializer.Serialize(input)}");

            try
            {
                Order order = new Order()
                {
                    Number = input.Number,
                    Total = input.Total,
                    CreatedAt = DateTime.Now,
                    IsCancelled = false,
                    Costumer = new Person { Id = input.Costumer!.Id, Name = input.Costumer!.Name, FederalId = input.Costumer!.FederalId, Email = input.Costumer!.Email },
                    BranchOffice = new BranchOffice { Id = input.BranchOffice!.Id, Name = input.BranchOffice!.Name }
                };
                order.Items = new List<OrderItem>();

                foreach (var item in input.Items??new List<OrderItemInput>()) 
                {
                    order.Items.Add(new OrderItem { 
                        ProductCode = item.ProductCode!,
                        ProductName = item.ProductName!,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Total = item.Total,
                        IsCancelled = false,
                        Order = order
                    });
                }
                
                _repository.Add<Order>(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateReceiver: {ex.Message}");
                throw;
            }
        }

        public void UpdateReceiver(object? sender, BasicDeliverEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Body.ToArray());
            OrderInput input = JsonSerializer.Deserialize<OrderInput>(content)!;

            _logger.LogInformation("Worker running Update at: {time}", DateTimeOffset.Now);
            _logger.LogInformation($"UpdateTask {JsonSerializer.Serialize(input)}");

            try
            {
                Order obj = _repository.List<Order>(0, 1, a => a.Id == input.Id)!.FirstOrDefault()!;

                obj.Number = input.Number;
                obj.Total = input.Total;
                obj.UpdatedAt = DateTime.Now;

                _repository.Update<Order>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateReceiver: {ex.Message}");
                throw;
            }
        }

        public void CancelReceiver(object? sender, BasicDeliverEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Body.ToArray());
            OrderInput input = JsonSerializer.Deserialize<OrderInput>(content)!;

            _logger.LogInformation("Worker running Delete at: {time}", DateTimeOffset.Now);
            _logger.LogInformation($"DeleteTask {JsonSerializer.Serialize(input)}");

            try
            {
                Order obj = _repository.List<Order>(0, 1, a => a.Id == input.Id)!.FirstOrDefault()!;
                obj.IsCancelled = true;
                _repository.Update<Order>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteReceiver: {ex.Message}");
                throw;
            }
        }

        public void CancelItemReceiver(object? sender, BasicDeliverEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Body.ToArray());
            OrderItemInput input = JsonSerializer.Deserialize<OrderItemInput>(content)!;

            _logger.LogInformation("Worker running Delete at: {time}", DateTimeOffset.Now);
            _logger.LogInformation($"DeleteTask {JsonSerializer.Serialize(input)}");

            try
            {
                OrderItem obj = _repository.List<OrderItem>(0, 1, a => a.Id == input.Id)!.FirstOrDefault()!;
                obj.IsCancelled = true;
                _repository.Update<OrderItem>(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteReceiver: {ex.Message}");
                throw;
            }
        }
    }
}
