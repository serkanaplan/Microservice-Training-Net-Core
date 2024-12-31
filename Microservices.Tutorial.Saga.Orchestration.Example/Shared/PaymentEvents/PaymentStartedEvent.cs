﻿using MassTransit;
using Shared.Messages;

namespace Shared.PaymentEvents;

public class PaymentStartedEvent(Guid correlationId) : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; } = correlationId;
    public decimal TotalPrice { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
}
