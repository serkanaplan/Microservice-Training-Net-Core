using MassTransit;
using Shared.Messages;

namespace Shared.OrderEvents;

public class OrderCreatedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
    public List<OrderItemMessage> OrderItems { get; set; }
}
