using Microsoft.EntityFrameworkCore;

namespace Order.API.Models.Contexts;

public class OrderAPIDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
