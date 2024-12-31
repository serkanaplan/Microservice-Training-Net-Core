using Shared.Datas;
namespace Shared.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public int BuyerId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    // idempotent sorunsalını çözmek için kullanılan token
    public Guid IdempotentToken { get; set; }
}
