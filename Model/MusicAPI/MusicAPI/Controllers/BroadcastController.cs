using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace MusicAPI.LoadData.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        private readonly IHubContext<HealthCheckHub> _hub;

        public BroadcastController(IHubContext<HealthCheckHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            await _hub.Clients.All.SendAsync("Update", "test");
            return Ok("Update Message sent");
        }
    }
}
