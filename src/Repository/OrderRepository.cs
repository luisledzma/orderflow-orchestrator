using orderflow.orchestrator.Data;
using orderflow.orchestrator.Models;

namespace orderflow.orchestrator.Repository;

/// <summary>
/// Implements the <see cref="IOrderRepository"/> interface for interacting with the Order data using Entity Framework.
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly OrderFlowDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class with the specified <see cref="OrderFlowDbContext"/>.
    /// </summary>
    /// <param name="context">The database context to interact with the database.</param>
    public OrderRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new order to the repository.
    /// </summary>
    /// <param name="order">The order entity to add.</param>
    /// <returns>The added order entity.</returns>
    public async Task<OrderModel> AddOrderAsync(OrderModel order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    /// <summary>
    /// Retrieves an order from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>The order with the specified ID, or null if not found.</returns>
    public async Task<OrderModel?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    /// <summary>
    /// Updates an existing order in the repository.
    /// </summary>
    /// <param name="order">The updated order entity.</param>
    /// <returns>The updated order entity.</returns>
    public async Task<OrderModel> UpdateOrderAsync(OrderModel order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    /// <summary>
    /// Deletes an existing order from the repository.
    /// </summary>
    /// <param name="order">The order entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteOrderAsync(OrderModel order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}

