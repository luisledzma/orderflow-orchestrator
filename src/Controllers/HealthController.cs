using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace orderflow.orchestrator.Controllers;
/// <summary>
/// This controller provides health checks for this microservice.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/orchestrator/health")]
[ApiVersion("1.0")]
public class HealthController : ControllerBase
{
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="HealthController"/> class.
    /// </summary>
    public HealthController()
    {
    }
    #endregion
    #region Actions
    /// <summary>
    /// Gets the current health status of the microservice.
    /// </summary>
    /// <returns>A 200 OK, indicating the microservice is healthy.</returns>
    /// <response code="200">Successful operation.</response>
    [HttpGet("GetStatus")]
    [Authorize(Roles = "Admin")] // 🔐 This requires JWT authentication
    public IActionResult GetStatus()
    {
        return Ok(new { status = "Orchestrator Healthy", timestamp = DateTime.UtcNow });
    }
    #endregion
}
