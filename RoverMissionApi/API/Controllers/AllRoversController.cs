using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("rovers")]
    public class RoversController : ControllerBase
    {
        private readonly IRoverService _roverService;

        public RoversController(IRoverService roverService)
        {
            _roverService = roverService;
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetRoversAsync()
        {
            var rovers = await _roverService.GetRoversAsync();
            return Ok(rovers);
        }

        [HttpGet("tasks")]    
        public async Task<IActionResult> GetAllRoversTasks([FromQuery] DateTime date)
        {
            var allTasks = await _roverService.GetAllDailyTasksAsync(date);
            return Ok(allTasks);
        }
    }
}
