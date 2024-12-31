using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers;

public class StockReservedEventConsumer(IPublishEndpoint publishEndpoint) : IConsumer<StockReservedEvent>
{
    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        // ödeme başarılı veya başarısız "mış" gibi yapıyoruz
        // ödeme işlemi başarısızsa status fail olacak ve stok servisi yaptığı işlemi geri alacak 
        // eğer stok miktarı yeterli değilse yine fail dönecek ve stok güncellenmeyecekktir 
        // stok miktrını tek bir ürün karşılamıyo olsa bile siparişteki diğer ürünlerin de işlemi geri alınır 
        if (true)
        {
            //Ödeme başarılı...
            PaymentCompletedEvent paymentCompletedEvent = new()
            {
                OrderId = context.Message.OrderId
            };
            await publishEndpoint.Publish(paymentCompletedEvent);
            await Console.Out.WriteLineAsync("Ödeme başarılı...");
        }
        else
        {
            //Ödeme başarısız...
            PaymentFailedEvent paymentFailedEvent = new()
            {
                OrderId = context.Message.OrderId,
                Message = "Yetersiz bakiye...",
                OrderItems = context.Message.OrderItems
            };
            await publishEndpoint.Publish(paymentFailedEvent);
            await Console.Out.WriteLineAsync("Ödeme başarısız...");
        }
    }
}
