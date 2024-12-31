using Microsoft.EntityFrameworkCore;
using Order.API.Consumers;
using Order.API.Models;
using MassTransit;
using Shared;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddMassTransit(conf =>
{
    conf.AddConsumer<PaymentCompletedEventConsumer>();
    conf.AddConsumer<StockNotReservedEventConsumer>();
    conf.AddConsumer<PaymentFailedEventConsumer>();

    conf.UsingRabbitMq((context, _conf) =>
    {
        _conf.Host(builder.Configuration["RabbitMQ"]);

        _conf.ReceiveEndpoint(RabbitMQSettings.Order_PaymentCompletedEventQueue, e => e.ConfigureConsumer<PaymentCompletedEventConsumer>(context));
        _conf.ReceiveEndpoint(RabbitMQSettings.Order_StockNotReservedEventQueue, e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));
        _conf.ReceiveEndpoint(RabbitMQSettings.Order_PaymentFailedEventQueue, e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
