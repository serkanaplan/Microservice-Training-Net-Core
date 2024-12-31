using Stock.API.Services;
using Shared.Events;
using Shared;
using MassTransit;
using MongoDB.Driver;

namespace Stock.API.Consumers;

public class OrderCreatedEventConsumer(MongoDBService mongoDBService, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        List<bool> stockResult = [];
        IMongoCollection<Models.Stock> collection = mongoDBService.GetCollection<Models.Stock>();

        foreach (var orderItem in context.Message.OrderItems)
        {                                                                                                                     // count alanını long olarak almayınca hata veriyor. şimdilik sebebini bilmiyorum
            stockResult.Add(await (await collection.FindAsync(s => s.ProductId == orderItem.ProductId.ToString() && s.Count > (long)orderItem.Count)).AnyAsync());
        }

        if (stockResult.TrueForAll(s => s.Equals(true)))
        {
            //Stock güncellemesi...
            foreach (var orderItem in context.Message.OrderItems)
            {
                Models.Stock stock = await (await collection.FindAsync(s => s.ProductId == orderItem.ProductId.ToString())).FirstOrDefaultAsync();
                stock.Count -= orderItem.Count;

                await collection.FindOneAndReplaceAsync(x => x.ProductId == orderItem.ProductId.ToString(), stock);
            }
            //Payment'ı uyaracak event'in fırlatılması...
            var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.Payment_StockReservedEventQueue}"));
            StockReservedEvent stockReservedEvent = new()
            {
                BuyerId = context.Message.BuyerId,
                OrderId = context.Message.OrderId,
                TotalPrice = context.Message.TotalPrice,
                OrderItems = context.Message.OrderItems,
            };
            // sadece belirli bir hedefe mesaj gönderme
            await sendEndpoint.Send(stockReservedEvent);
        }
        else
        {
            //Stok işlemi başarısız...
            //Order'ı uyaracak event fırlatılacaktır.
            StockNotReservedEvent stockNotReservedEvent = new()
            {
                BuyerId = context.Message.BuyerId,
                OrderId = context.Message.OrderId,
                Message = "Stok miktarı yetersiz..."
            };

            await publishEndpoint.Publish(stockNotReservedEvent);
        }
    }
}
