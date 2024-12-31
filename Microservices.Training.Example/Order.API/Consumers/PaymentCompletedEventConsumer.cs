using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared.Events;

namespace Order.API.Consumers;

public class PaymentCompletedEventConsumer(OrderAPIDbContext orderAPIDbContext) : IConsumer<PaymentCompletedEvent>
{
    readonly OrderAPIDbContext _orderAPIDbContext = orderAPIDbContext;

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        Models.Entities.Order order = await _orderAPIDbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == context.Message.OrderId);
        order.OrderStatus = Models.Enums.OrderStatus.Completed;
        await _orderAPIDbContext.SaveChangesAsync();
    }
}
