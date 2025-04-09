using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orderflow.orchestrator.Models;
using orderflow.orchestrator.Services;
using System.Threading.Tasks;

namespace orderflow.orchestrator.Controllers;

/// <summary>
/// This controller provides CRUD operations for this microservice.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/orchestrator/order")]
[ApiVersion("1.0")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="entity">The order to create.</param>
    /// <returns>A 201 Created response with the created order.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")] // 🔐 This requires JWT authentication
    [ProducesResponseType(typeof(OrderModel), 201)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddProduct([FromBody] OrderModel entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.AddOrderAsync(entity);
        return CreatedAtAction(nameof(AddProduct), new { id = createdOrder.Id }, createdOrder);
    }
}
