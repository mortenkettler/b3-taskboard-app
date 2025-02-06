using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTOs;
using TaskBoard.Application.Interfaces;

// Add more Swagger docs?
namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListsController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        /// <summary>
        /// Retrieves all task lists.
        /// GET: api/tasklists
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var taskLists = _taskListService.GetAllTaskLists();
            return Ok(taskLists);
        }

        /// <summary>
        /// Retrieves a specific task list by its ID.
        /// GET: api/tasklists/{id}
        /// </summary>
        /// <param name="id">The ID of the task list.</param>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var taskList = _taskListService.GetTaskListById(id);
            if (taskList == null)
            {
                return NotFound();
            }
            return Ok(taskList);
        }

        /// <summary>
        /// Creates a new task list.
        /// POST: api/tasklists
        /// </summary>
        /// <param name="taskListDTO">a data transfer object representing the new task list.</param>
        [HttpPost]
        public IActionResult Create([FromBody] TaskListDTO taskListDTO)
        {
            var createdTaskList = _taskListService.CreateTaskList(taskListDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdTaskList.Id }, createdTaskList);
        }

        /// <summary>
        /// Updates an existing task list.
        /// PUT: api/tasklists/{id}
        /// </summary>
        /// <param name="id">The ID of the task list to update.</param>
        /// <param name="taskListDTO">The updated task list data.</param>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskListDTO taskListDTO)
        {
            if (id != taskListDTO.Id)
            {
                return BadRequest("ID does not match.");
            }

            _taskListService.UpdateTaskList(taskListDTO);
            return NoContent();
        }

        /// <summary>
        /// Deletes a task list.
        /// DELETE: api/tasklists/{id}
        /// </summary>
        /// <param name="id">The ID of the task list to delete.</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskListService.DeleteTaskList(id);
            return NoContent();
        }
    }
}
