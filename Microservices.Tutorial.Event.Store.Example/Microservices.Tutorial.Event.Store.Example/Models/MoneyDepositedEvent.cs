namespace Microservices.Tutorial.Event.Store.Example.Models;

class MoneyDepositedEvent
{
    public string AccountId { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}
