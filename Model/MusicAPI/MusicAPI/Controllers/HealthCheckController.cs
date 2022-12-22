using Microsoft.AspNetCore.Mvc;

namespace MusicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> GetHealth() => "Running";

}