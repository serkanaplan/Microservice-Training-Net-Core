﻿using System.ComponentModel.DataAnnotations;

namespace Order.API.Models.Entities;

public class OrderOutbox
{
    // idempotent sorunsalını çözmek için kullanılan token
    [Key]
    public Guid IdempotentToken { get; set; }
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string Type { get; set; }
    public string Payload { get; set; }
}
