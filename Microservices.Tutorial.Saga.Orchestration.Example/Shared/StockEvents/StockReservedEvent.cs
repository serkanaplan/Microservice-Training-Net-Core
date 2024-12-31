using MassTransit;
using Shared.Messages;

namespace Shared.StockEvents;

public class StockReservedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
    public List<OrderItemMessage> OrderItems { get; set; }
}
