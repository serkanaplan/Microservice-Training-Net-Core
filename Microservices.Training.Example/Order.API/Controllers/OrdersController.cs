using Order.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Order.API.ViewModels;
using Order.API.Models;
using Shared.Events;

using MassTransit;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(OrderAPIDbContext context, IPublishEndpoint publishEndpoint) : ControllerBase
{
    readonly OrderAPIDbContext _context = context;
    readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderVM createOrder)
    {
        Models.Entities.Order order = new()
        {
            OrderId = Guid.NewGuid(),
            BuyerId = createOrder.BuyerId,
            CreatedDate = DateTime.Now,
            OrderStatus = Models.Enums.OrderStatus.Suspend,
            OrderItems = createOrder.OrderItems.Select(oi => new OrderItem
            {
                Count = oi.Count,
                Price = oi.Price,
                ProductId = oi.ProductId,
            }).ToList(),

            TotalPrice = createOrder.OrderItems.Sum(oi => oi.Price * oi.Count)
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();  

        OrderCreatedEvent orderCreatedEvent = new()
        {
            BuyerId = order.BuyerId,
            OrderId = order.OrderId,
            OrderItems = order.OrderItems
            .Select(oi => new Shared.Messages.OrderItemMessage{Count = oi.Count,ProductId = oi.ProductId}).ToList(),
            TotalPrice = order.TotalPrice
        };

        await _publishEndpoint.Publish(orderCreatedEvent);
        return Ok();
    }
}
