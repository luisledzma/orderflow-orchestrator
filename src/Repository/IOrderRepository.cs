using orderflow.orchestrator.Models;
using System.Threading.Tasks;

namespace orderflow.orchestrator.Repository;
public interface IOrderRepository
{
    Task<OrderModel> AddOrderAsync(OrderModel order);
}
