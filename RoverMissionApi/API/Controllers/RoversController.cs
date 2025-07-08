// Controlador Api para cada Rover
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("rovers/{roverName}/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly IRoverService _roverService;

        public TasksController(IRoverService roverService)
        {
            _roverService = roverService ?? throw new ArgumentNullException(nameof(roverService));
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(string roverName, [FromBody] RoverTask task)
        {
            if (!Enum.IsDefined(typeof(TaskType), task.TaskType))
                return BadRequest("Invalid TaskType");

            if (!Enum.IsDefined(typeof(Status), task.Status))
                return BadRequest("Invalid Status");

            if (task.RoverName != roverName)
                return BadRequest("RoverName mismatch");

            await _roverService.AddTaskAsync(task);

            return Created($"/rovers/{roverName}/tasks?date={task.StartsAt:yyyy-MM-dd}", task);

        }

        [HttpGet]
        public async Task<IActionResult> GetDailyTasks(string roverName, [FromQuery] DateTime date)
            => Ok(await _roverService.GetDailyTasksAsync(roverName, date));

        [HttpGet("/rovers/{roverName}/utilization")]
        public async Task<IActionResult> GetUtilization(string roverName, [FromQuery] DateTime date)
            => Ok(await _roverService.CalculateUtilizationAsync(roverName, date));

        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllRoversTasks([FromQuery] DateTime date)
        {
            var allTasks = await _roverService.GetAllDailyTasksAsync(date);
            return Ok(allTasks);
        }
    }
    
        
}
