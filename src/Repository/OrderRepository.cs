
using orderflow.orchestrator.Data;
using orderflow.orchestrator.Models;
using Microsoft.EntityFrameworkCore;

namespace orderflow.orchestrator.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly OrderFlowDbContext _context;

    public OrderRepository(OrderFlowDbContext context)
    {
        _context = context;
    }

    public async Task<OrderModel> AddOrderAsync(OrderModel order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
