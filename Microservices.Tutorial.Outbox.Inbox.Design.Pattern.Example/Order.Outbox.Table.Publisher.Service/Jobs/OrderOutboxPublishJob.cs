using Order.Outbox.Table.Publisher.Service.Entities;
using System.Text.Json;
using Shared.Events;
using MassTransit;
using Quartz;

namespace Order.Outbox.Table.Publisher.Service.Jobs;

public class OrderOutboxPublishJob(IPublishEndpoint publishEndpoint) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (OrderOutboxSingletonDatabase.DataReaderState)
        {
            OrderOutboxSingletonDatabase.DataReaderBusy();

            List<OrderOutbox> orderOutboxes = (await OrderOutboxSingletonDatabase.QueryAsync<OrderOutbox>($@"SELECT * FROM ORDEROUTBOXES WHERE PROCESSEDDATE IS NULL ORDER BY OCCUREDON ASC")).ToList();

            foreach (var orderOutbox in orderOutboxes)
            {
                if (orderOutbox.Type == nameof(OrderCreatedEvent))
                {
                    OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderOutbox.Payload);
                    if (orderCreatedEvent != null)
                    {
                        await publishEndpoint.Publish(orderCreatedEvent);
                        OrderOutboxSingletonDatabase.ExecuteAsync($"UPDATE ORDEROUTBOXES SET PROCESSEDDATE = GETDATE() WHERE IdempotentToken = '{orderOutbox.IdempotentToken}'");
                    }
                }
            }

            OrderOutboxSingletonDatabase.DataReaderReady();
            await Console.Out.WriteLineAsync("Order outbox table checked!");
        }
    }
}
