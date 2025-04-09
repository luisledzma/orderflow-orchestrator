using Microsoft.EntityFrameworkCore;
using orderflow.orchestrator.Models;

namespace orderflow.orchestrator.Data;

public class OrderFlowDbContext : DbContext
{
    public OrderFlowDbContext(DbContextOptions<OrderFlowDbContext> options) : base(options) { }

    public DbSet<OrderModel> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
