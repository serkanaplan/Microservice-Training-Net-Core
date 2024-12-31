using MassTransit;

namespace Shared.PaymentEvents;

public class PaymentCompletedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
}
