using Microsoft.EntityFrameworkCore;
using MiniInventoryManagementSystem.Domain.Interfaces;
using MiniInventoryManagementSystem.Domain.Services;
using MiniInventoryManagementSystem.Infra.Context;
using MiniInventoryManagementSystem.Infra.Repositories;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MiniInventoryManagementSystemCtx>(options =>
{
    var mainConnecttion = builder.Configuration.GetConnectionString("sqlServer");
    options.UseSqlServer(mainConnecttion);
});

builder.Services.AddScoped<IRepository, Repository>(serviceProvider =>
{
    var context = serviceProvider.GetService<MiniInventoryManagementSystemCtx>()!;
    context.Database.SetCommandTimeout(400);
    return new Repository(context);
});


builder.Services.AddTransient<IMessageBus, MessageBus>(serviceProvider => {

    ConnectionFactory factory = new ConnectionFactory();
    factory.HostName = builder.Configuration.GetValue<String>("RabbitMQ:HostName");
    factory.VirtualHost = builder.Configuration.GetValue<String>("RabbitMQ:VirtualHost");
    factory.Port = builder.Configuration.GetValue<int>("RabbitMQ:Port");
    factory.UserName = builder.Configuration.GetValue<String>("RabbitMQ:UserName");
    factory.Password = builder.Configuration.GetValue<String>("RabbitMQ:Password");
    return new MessageBus(factory);
});

builder.Services.AddTransient<IOrderService, OrderService>();


builder.Services.AddCors(a => a.AddPolicy("General", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();

Log.CloseAndFlush();
