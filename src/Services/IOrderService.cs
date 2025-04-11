using orderflow.orchestrator.Models;

namespace orderflow.orchestrator.Services;
public interface IOrderService
{
    /// <summary>
    /// Validates and sends the Order entity to the repository to be added.
    /// </summary>
    /// <param name="order">Order entity to validate and send.</param>
    /// <returns>The created Order object.</returns>
    Task<OrderModel> AddOrderAsync(OrderModel order);

    /// <summary>
    /// Partially updates an existing order.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="updatedFields">The fields to update.</param>
    /// <returns>The updated Order object, or null if not found.</returns>
    Task<OrderModel?> UpdateOrderAsync(int id, OrderModel updatedFields);

    /// <summary>
    /// Deletes an existing order by ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>True if the order was deleted, false if not found.</returns>
    Task<bool> DeleteOrderAsync(int id);
}