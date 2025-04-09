using orderflow.orchestrator.Models;
using orderflow.orchestrator.Repository;

namespace orderflow.orchestrator.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderModel> AddOrderAsync(OrderModel order)
    {
        // Business logic/validation can go here
        if (string.IsNullOrEmpty(order.CustomerName))
        {
            throw new ArgumentException("Customer name is required");
        }

        return await _orderRepository.AddOrderAsync(order);
    }
}
