using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using orderflow.orchestrator.Models;
using orderflow.orchestrator.Services;

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
    /// <returns>A 200 Ok response with the created order.</returns>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")] // 🔐 This requires JWT authentication
    [ProducesResponseType(typeof(OrderModel), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> AddOrder([FromBody] OrderModel entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.AddOrderAsync(entity);
        //return CreatedAtAction(nameof(AddOrder), new { id = createdOrder.Id }, createdOrder);
        return Ok(createdOrder);
    }

    /// <summary>
    /// Partially updates an existing order.
    /// </summary>
    /// <param name="id">The ID of the order to update.</param>
    /// <param name="updatedFields">The fields to update.</param>
    /// <returns>A 200 OK with the updated order, or 404 if not found.</returns>
    [HttpPatch("update/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(OrderModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderModel updatedFields)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedOrder = await _orderService.UpdateOrderAsync(id, updatedFields);

        if (updatedOrder == null)
        {
            return NotFound();
        }

        return Ok(updatedOrder);
    }

    /// <summary>
    /// Deletes an existing order.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>A 200 Ok if the order was deleted, or 404 if not found.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var success = await _orderService.DeleteOrderAsync(id);

        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }


}
