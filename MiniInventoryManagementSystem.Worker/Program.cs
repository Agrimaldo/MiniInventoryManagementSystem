// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Domain.Services;
using MiniInventoryManagementSystem.Infra.Context;
using MiniInventoryManagementSystem.Infra.Repositories;
using MiniInventoryManagementSystem.Worker;
using RabbitMQ.Client;
using Serilog;

Console.WriteLine("Hello, World!");

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var optionsBuilder = new DbContextOptionsBuilder<MiniInventoryManagementSystemCtx>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("sqlServer"));
        services.AddTransient<MiniInventoryManagementSystemCtx>(s => new MiniInventoryManagementSystemCtx(optionsBuilder.Options));
        services.AddTransient<IRepository, Repository>(serviceProvider =>
        {
            var context = serviceProvider.GetService<MiniInventoryManagementSystemCtx>()!;
            context.Database.SetCommandTimeout(400);
            return new Repository(context);
        });
        services.AddTransient<IMessageBus, MessageBus>(serviceProvider => {

            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = configuration.GetValue<String>("RabbitMQ:HostName");
            factory.VirtualHost = configuration.GetValue<String>("RabbitMQ:VirtualHost");
            factory.Port = configuration.GetValue<int>("RabbitMQ:Port");
            factory.UserName = configuration.GetValue<String>("RabbitMQ:UserName");
            factory.Password = configuration.GetValue<String>("RabbitMQ:Password");
            return new MessageBus(factory);
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
Log.CloseAndFlush();
