﻿using Microsoft.EntityFrameworkCore;
using Order.API.Models.Entities;

namespace Order.API.Models.Contexts;

public class OrderDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderOutbox> OrderOutboxes { get; set; }

}
