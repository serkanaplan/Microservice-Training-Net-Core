using MassTransit;

namespace Shared.StockEvents;

public class StockNotReservedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
    public string Message { get; set; }
}
