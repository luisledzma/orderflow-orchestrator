using Microsoft.EntityFrameworkCore;
using orderflow.orchestrator.Models;

namespace orderflow.orchestrator.Data;

/// <summary>
/// The Entity Framework database context for the OrderFlow orchestrator microservice.
/// </summary>
public class OrderFlowDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderFlowDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public OrderFlowDbContext(DbContextOptions<OrderFlowDbContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the orders table in the database.
    /// </summary>
    public DbSet<OrderModel> Orders { get; set; }

    /// <summary>
    /// Configures the entity framework model.
    /// </summary>
    /// <param name="modelBuilder">Provides a simple API surface for configuring the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional entity configurations can be added here if needed.
    }
}
