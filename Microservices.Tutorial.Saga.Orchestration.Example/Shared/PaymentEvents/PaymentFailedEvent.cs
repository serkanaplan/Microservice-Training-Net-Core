using MassTransit;
using Shared.Messages;

namespace Shared.PaymentEvents;

public class PaymentFailedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
    public string Message { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
}
