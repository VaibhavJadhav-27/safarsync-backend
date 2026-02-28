using Microsoft.AspNetCore.Mvc;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                TripId = request.TripId,
                Title = request.Title,
                AssignedTo = request.AssignedTo,
                DueDate = request.DueDate,
                CreatedBy = request.CreatedBy
            };

            await _taskRepository.AddTask(task);

            return Ok(task);
        }

        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetTasks(Guid tripId)
        {
            var tasks = await _taskRepository.GetTasksByTrip(tripId);
            return Ok(tasks);
        }

        [HttpPut("{taskId}/complete")]
        public async Task<IActionResult> CompleteTask(Guid taskId)
        {
            await _taskRepository.MarkComplete(taskId);
            return Ok("Task completed");
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            await _taskRepository.DeleteTask(taskId);
            return Ok("Task deleted");
        }
    }
}
