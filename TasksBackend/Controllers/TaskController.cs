using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksBackend.Data;
using TasksBackend.Models;

namespace TasksBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route will be "api/task"
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor to inject the DbContext
        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/task
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/task
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TodoTask task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate a new TaskId if not provided
            if (string.IsNullOrEmpty(task.TaskId))
            {
                task.TaskId = Guid.NewGuid().ToString();
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Return 201 Created with the new resource location
            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(string id, [FromBody] TodoTask updatedTask)
        {
            if (id != updatedTask.TaskId)
            {
                return BadRequest("Task ID mismatch");
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            // Update properties
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Completed = updatedTask.Completed;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(t => t.TaskId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent(); // 204 No Content for successful update
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content for successful deletion
        }
    }   
}