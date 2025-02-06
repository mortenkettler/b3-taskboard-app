using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTOs;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// GET: api/tasks
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// Retrieves a specific task by its ID.
        /// GET: api/tasks/{id}
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        /// <summary>
        /// Creates a new task.
        /// POST: api/tasks
        /// </summary>
        /// <param name="taskDTO">A data transfer object representing the new task.</param>
        [HttpPost]
        public IActionResult Create([FromBody] TaskDTO taskDTO)
        {
            var createdTask = _taskService.CreateTask(taskDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Updates an existing task.
        /// PUT: api/tasks/{id}
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="taskDTO">The updated task data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskDTO taskDTO)
        {
            if (id != taskDTO.Id)
            {
                return BadRequest("ID mismatch");
            }
            _taskService.UpdateTask(taskDTO);
            return NoContent();
        }

        /// <summary>
        /// Deletes a task.
        /// DELETE: api/tasks/{id}
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
