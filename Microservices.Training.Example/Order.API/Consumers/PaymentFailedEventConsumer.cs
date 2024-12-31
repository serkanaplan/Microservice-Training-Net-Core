﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers;

public class PaymentFailedEventConsumer(OrderAPIDbContext orderAPIDbContext) : IConsumer<PaymentFailedEvent>
{
    readonly OrderAPIDbContext _orderAPIDbContext = orderAPIDbContext;

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        Models.Entities.Order order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);
        order.OrderStatus = Models.Enums.OrderStatus.Failed;
        await _orderAPIDbContext.SaveChangesAsync();
    }
}
