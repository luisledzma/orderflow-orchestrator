using orderflow.orchestrator.Models;
using orderflow.orchestrator.Repository;

namespace orderflow.orchestrator.Services;

/// <summary>
/// Service class that handles business logic and operations related to orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="orderRepository">The repository instance for order data access.</param>
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Adds a new order after applying business rules or validations.
    /// </summary>
    /// <param name="order">The order model to be added.</param>
    /// <returns>The created <see cref="OrderModel"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when required fields are missing.</exception>
    public async Task<OrderModel> AddOrderAsync(OrderModel order)
    {
        // Business logic/validation can go here
        if (string.IsNullOrEmpty(order.CustomerName))
        {
            throw new ArgumentException("Customer name is required");
        }

        return await _orderRepository.AddOrderAsync(order);
    }

    /// <summary>
    /// Updates an existing order with new field values.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="updatedFields">An <see cref="OrderModel"/> containing the fields to update.</param>
    /// <returns>The updated <see cref="OrderModel"/> if found; otherwise, <c>null</c>.</returns>
    public async Task<OrderModel?> UpdateOrderAsync(int id, OrderModel updatedFields)
    {
        var existingOrder = await _orderRepository.GetOrderByIdAsync(id);

        if (existingOrder == null)
        {
            return null;
        }

        // Update only the fields that are not null or default
        if (!string.IsNullOrWhiteSpace(updatedFields.CustomerName))
            existingOrder.CustomerName = updatedFields.CustomerName;

        if (updatedFields.TotalAmount > 0)
            existingOrder.TotalAmount = updatedFields.TotalAmount;

        return await _orderRepository.UpdateOrderAsync(existingOrder);
    }

    /// <summary>
    /// Deletes an order by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns><c>true</c> if the order was found and deleted; otherwise, <c>false</c>.</returns>
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);

        if (order == null)
        {
            return false;
        }

        await _orderRepository.DeleteOrderAsync(order);
        return true;
    }
}
