using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApplication1.Data;
using SampleWebApplication1.Models;

namespace SampleWebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoController> _logger;

        public TodoController(TodoContext context, ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            _logger.LogInformation("Getting all todos");
            return await _context.Todos.ToListAsync();
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            _logger.LogInformation("Getting todo with id {Id}", id);
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found", id);
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            return todo;
        }

        // POST: api/todo
        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(TodoCreateDto todoDto)
        {
            var todo = new Todo
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsCompleted = false,
                CreatedDate = DateTime.UtcNow
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created new todo with id {Id}", todo.Id);

            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoUpdateDto todoDto)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found for update", id);
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            todo.Title = todoDto.Title;
            todo.Description = todoDto.Description;
            todo.IsCompleted = todoDto.IsCompleted;

            if (todoDto.IsCompleted && todo.CompletedDate == null)
            {
                todo.CompletedDate = DateTime.UtcNow;
            }
            else if (!todoDto.IsCompleted)
            {
                todo.CompletedDate = null;
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated todo with id {Id}", id);

            return NoContent();
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found for deletion", id);
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted todo with id {Id}", id);

            return NoContent();
        }

        // GET: api/todo/completed
        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetCompletedTodos()
        {
            _logger.LogInformation("Getting completed todos");
            return await _context.Todos.Where(t => t.IsCompleted).ToListAsync();
        }

        // GET: api/todo/pending
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetPendingTodos()
        {
            _logger.LogInformation("Getting pending todos");
            return await _context.Todos.Where(t => !t.IsCompleted).ToListAsync();
        }
    }

    // DTOs for cleaner API contracts
    public record TodoCreateDto(string Title, string? Description);
    public record TodoUpdateDto(string Title, string? Description, bool IsCompleted);
}
