using Order.API.Models.Contexts;
using Shared.Events;
using MassTransit;

namespace Order.API.Consumers;

public class PaymentCompletedEventConsumer(OrderAPIDbContext _context) : IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var order = await _context.Orders.FindAsync(context.Message.OrderId) ?? throw new NullReferenceException();
        order.OrderStatus = Enums.OrderStatus.Completed;
        await _context.SaveChangesAsync();
    }
}
