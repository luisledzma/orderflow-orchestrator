using orderflow.orchestrator.Models;
using System.Threading.Tasks;

namespace orderflow.orchestrator.Services;
public interface IOrderService
{
    /// <summary>
    /// Validates and sends the Order entity to the repository to be added.
    /// </summary>
    /// <param name="order">Order entity to validate and send.</param>
    /// <returns>The created Order object.</returns>
    Task<OrderModel> AddOrderAsync(OrderModel order);
}