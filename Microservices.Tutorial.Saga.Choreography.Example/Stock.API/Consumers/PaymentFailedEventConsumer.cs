using Stock.API.Services;
using MongoDB.Driver;
using Shared.Events;
using MassTransit;

namespace Stock.API.Consumers;

// ödeme başarılı veya başarısız "mış" gibi yapıyoruz
// ödeme işlemi başarısızsa status fail olacak ve stok servisi yaptığı işlemi geri alacak 
// eğer stok miktarı yeterli değilse yine fail dönecek ve stok güncellenmeyecekktir 
// stok miktrını tek bir ürün karşılamıyo olsa bile siparişteki diğer ürünlerin de işlemi geri alınır 
public class PaymentFailedEventConsumer(MongoDBService mongoDBService) : IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var stocks = mongoDBService.GetCollection<Models.Stock>();
        foreach (var orderItem in context.Message.OrderItems)
        {
            var stock = await (await stocks.FindAsync(s => s.ProductId == orderItem.ProductId.ToString())).FirstOrDefaultAsync();
            if (stock != null)
            {
                stock.Count += orderItem.Count;
                await stocks.FindOneAndReplaceAsync(s => s.ProductId == orderItem.ProductId.ToString(), stock);
            }
        }
    }
}
