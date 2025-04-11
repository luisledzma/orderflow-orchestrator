namespace orderflow.orchestrator.Models;

/// <summary>
/// Represents an order in the system.
/// </summary>
public class OrderModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer who placed the order.
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the total amount for the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp of the order. 
    /// Defaults to the current UTC time if not specified.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

