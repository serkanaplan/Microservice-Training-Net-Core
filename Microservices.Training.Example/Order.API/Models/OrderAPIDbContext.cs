using Microsoft.EntityFrameworkCore;
using Order.API.Models.Entities;

namespace Order.API.Models;

public class OrderAPIDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Entities.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
