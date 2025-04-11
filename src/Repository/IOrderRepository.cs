using orderflow.orchestrator.Models;

namespace orderflow.orchestrator.Repository;

/// <summary>
/// Defines the contract for interacting with the Order data in the repository.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Adds a new order to the repository.
    /// </summary>
    /// <param name="order">The order entity to be added.</param>
    /// <returns>The added order entity.</returns>
    Task<OrderModel> AddOrderAsync(OrderModel order);

    /// <summary>
    /// Retrieves an order from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>The order with the specified ID, or null if not found.</returns>
    Task<OrderModel?> GetOrderByIdAsync(int id);

    /// <summary>
    /// Updates an existing order in the repository.
    /// </summary>
    /// <param name="updatedOrder">The updated order entity.</param>
    /// <returns>The updated order entity.</returns>
    Task<OrderModel> UpdateOrderAsync(OrderModel updatedOrder);

    /// <summary>
    /// Deletes an order from the repository.
    /// </summary>
    /// <param name="order">The order entity to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteOrderAsync(OrderModel order);
}

