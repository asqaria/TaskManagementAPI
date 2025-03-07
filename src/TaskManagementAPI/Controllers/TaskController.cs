using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Dtos;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Extensions;
using MediatR;
using TaskManagementAPI.Services.Queries;
using TaskManagementAPI.Services.Commands;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TaskManagementAPI.Hubs;
using TaskManagementAPI.Services.Interface;
using MassTransit;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRedisService redisService;
        private readonly IHubContext<TaskHub> hubContext;
        private readonly RabbitMQService rabbitMQ;

        public TaskController(IMediator mediator, IRedisService redisService, IHubContext<TaskHub> hubContext, RabbitMQService rabbitMQ)
        {
            this._mediator = mediator;
            this.redisService = redisService;
            this.hubContext = hubContext;
            this.rabbitMQ = rabbitMQ;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTasks(string? statusFilter)
        {
            string filter = statusFilter;
            if(string.IsNullOrEmpty(statusFilter)) filter = "All";
            Log.Information("Getting tasks with status filter: {StatusFilter}", statusFilter);

            var result = await redisService.GetAsync(filter);
            if (result == null)
            {
                result = await _mediator.Send(new GetTasksQuery(statusFilter));
            }
            
            if (!result.Any())
            {
                Log.Information("No tasks found with status filter: {StatusFilter}", statusFilter);
                return NotFound();
            }

            Log.Information("Found {Count} tasks with status filter: {StatusFilter}", result.Count(), statusFilter);
            await redisService.SetAsync(filter, result, TimeSpan.FromMinutes(1));

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid id)
        {
            Log.Information("Getting task with ID: {Id}", id);
            var result = await _mediator.Send(new GetTaskByIdQuery(id));
            if (result is null)
            {
                Log.Warning("Task with ID: {Id} not found", id);
                return NotFound();
            }
            Log.Information("Task with ID: {Id} found", id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto taskDto)
        {
            Log.Information("Creating a new task with title: {Title}", taskDto.Title);
            TaskEntity task = new()
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = taskDto.Status
            };
            try
            {
                var result = await _mediator.Send(new CreateTaskCommand(task));
                await hubContext.Clients.All.SendAsync("RecieveMessage", taskDto.Title, taskDto.Description, taskDto.Status);
                rabbitMQ.SendMessage(taskDto, "createdTasks");
                Log.Information("Task created with ID: {Id}", result.Id);
                return CreatedAtAction(nameof(GetTask), new { id = result.Id }, task.asDto());
            }
            catch (Exception ex)
            {
                Log.Error("Error creating task with title: {Title}", taskDto.Title);
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(Guid id, string status)
        {
            Log.Information("Updating task with ID: {Id} to status: {Status}", id, status);
            var existingTask = await _mediator.Send(new GetTaskByIdQuery(id));
            if (existingTask is null)
            {
                Log.Warning("Task with ID: {Id} not found", id);
                return NotFound();
            } else if (string.IsNullOrEmpty(status))
            {
                Log.Warning("Status cannot be empty");
                return BadRequest("Status cannot be empty");
            }

            await _mediator.Send(new UpdateTaskCommand(id, status));
            Log.Information("Task with ID: {Id} updated to status: {Status}", id, status);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            Log.Information("Deleting task with ID: {Id}", id);
            var existingTask = await _mediator.Send(new GetTaskByIdQuery(id));
            if (existingTask is null)
            {
                Log.Warning("Task with ID: {Id} not found", id);
                return NotFound();
            }

            await _mediator.Send(new DeleteTaskCommand(id));
            Log.Information("Task with ID: {Id} deleted", id);
            return NoContent();
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            Log.Information("Health check");
            return Ok("Healthy");
        }
    }
}