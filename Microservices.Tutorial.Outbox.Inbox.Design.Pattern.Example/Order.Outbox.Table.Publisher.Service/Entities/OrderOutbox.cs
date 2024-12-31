namespace Order.Outbox.Table.Publisher.Service.Entities;

public class OrderOutbox
{
    public Guid IdempotentToken { get; set; }
    public DateTime OccuredOn { get; set; } //kayıt tarihi 
    public DateTime? ProcessedDate { get; set; } // kaydın işlenme tarihi 
    public string Type { get; set; }
    public string Payload { get; set; }
}
