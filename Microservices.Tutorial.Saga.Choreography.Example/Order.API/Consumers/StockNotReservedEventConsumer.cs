using Order.API.Models.Contexts;
using Shared.Events;
using MassTransit;

namespace Order.API.Consumers;

public class StockNotReservedEventConsumer(OrderAPIDbContext _context) : IConsumer<StockNotReservedEvent>
{
    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        var order = await _context.Orders.FindAsync(context.Message.OrderId) ?? throw new NullReferenceException();
        order.OrderStatus = Enums.OrderStatus.Fail;
        await _context.SaveChangesAsync();
    }
}
