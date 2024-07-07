using Microsoft.AspNetCore.Mvc;
using TodoList_CRUD_Practice.Services;
using TodoList_CRUD_Practice.Models;

namespace TodoList_CRUD_Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private readonly ITaskService _taskService;
        public ToDoController(ITaskService taskService) {
            this._taskService = taskService;
                }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await this._taskService.GetAllTask());
        }

        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            return Ok(await this._taskService.GetTaskById(id));
        }

        [HttpPost("task")]
        public async Task<IActionResult> AddTask([FromBody] Tasks task)
        {
            int GeneratedId = await this._taskService.AddTask(task);

            if (GeneratedId > 0)
            {
                return Created();
            }
            return BadRequest();

        }


        [HttpPut("task")]
        public async Task<IActionResult> UpdateTask([FromBody] Tasks task)
        {
            int RowsUpdated = await this._taskService.UpdateTask(task);

            if (RowsUpdated > 0)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("task/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            int RowsUpdated = await this._taskService.DeleteTask(id);

            if (RowsUpdated > 0)
            {
                return NoContent();
            }
            return BadRequest();

        }
    }
}
