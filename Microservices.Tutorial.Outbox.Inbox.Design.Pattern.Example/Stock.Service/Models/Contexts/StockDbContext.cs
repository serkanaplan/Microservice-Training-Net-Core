using Microsoft.EntityFrameworkCore;
using Stock.Service.Models.Entities;

namespace Stock.Service.Models.Contexts;

public class StockDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<OrderInbox> OrderInboxes { get; set; }
}
