﻿using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stock.Service.Models.Contexts;
using Stock.Service.Models.Entities;

namespace Stock.Service.Consumers;

public class OrderCreatedEventConsumer(StockDbContext stockDbContext) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var result = await stockDbContext.OrderInboxes.AnyAsync(i => i.IdempotentToken == context.Message.IdempotentToken);
        if (!result)
        {
            await stockDbContext.OrderInboxes.AddAsync(new()
            {
                Processed = false,
                Payload = JsonSerializer.Serialize(context.Message),
                // tokeni meajdan gelen token ile eşitlemeliyiz yoksa guid otomatik kendisi oluşturur. bu durumda order ve stock arasında eşleşme sağlanamaz.(tokenlar(guid degerleri) farklı olduğu için)
                IdempotentToken = context.Message.IdempotentToken
            });

            await stockDbContext.SaveChangesAsync();
        }


        List<OrderInbox> orderInboxes = await stockDbContext.OrderInboxes.Where(i => i.Processed == false).ToListAsync();
        foreach (var orderInbox in orderInboxes)
        {
            OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderInbox.Payload);
            Console.WriteLine($"{orderCreatedEvent.OrderId} order id değerine karşılık olan siparişin stok işlemleri başarıyla tamamlanmıştır.");
            orderInbox.Processed = true;
            await stockDbContext.SaveChangesAsync();
        }
    }
}
