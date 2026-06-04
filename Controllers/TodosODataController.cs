using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SampleWebApplication1.Data;
using SampleWebApplication1.Models;

namespace SampleWebApplication1.Controllers
{
    public class TodosController : ODataController
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodosController> _logger;

        public TodosController(TodoContext context, ILogger<TodosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /odata/Todos
        [EnableQuery]
        public IActionResult Get()
        {
            _logger.LogInformation("OData query on Todos");
            return Ok(_context.Todos);
        }

        // GET: /odata/Todos(5)
        [EnableQuery]
        public IActionResult Get(int key)
        {
            _logger.LogInformation("Getting todo with id {Id} via OData", key);
            var todo = _context.Todos.FirstOrDefault(t => t.Id == key);

            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found", key);
                return NotFound();
            }

            return Ok(todo);
        }

        // POST: /odata/Todos
        public IActionResult Post([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            todo.CreatedDate = DateTime.UtcNow;
            _context.Todos.Add(todo);
            _context.SaveChanges();

            _logger.LogInformation("Created new todo with id {Id} via OData", todo.Id);

            return Created(todo);
        }

        // PATCH: /odata/Todos(5)
        public IActionResult Patch(int key, [FromBody] Microsoft.AspNetCore.OData.Deltas.Delta<Todo> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = _context.Todos.FirstOrDefault(t => t.Id == key);
            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found for patch", key);
                return NotFound();
            }

            delta.Patch(todo);
            _context.SaveChanges();

            _logger.LogInformation("Patched todo with id {Id} via OData", key);

            return Updated(todo);
        }

        // PUT: /odata/Todos(5)
        public IActionResult Put(int key, [FromBody] Todo update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = _context.Todos.FirstOrDefault(t => t.Id == key);
            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found for update", key);
                return NotFound();
            }

            todo.Title = update.Title;
            todo.Description = update.Description;
            todo.IsCompleted = update.IsCompleted;
            todo.CompletedDate = update.CompletedDate;

            _context.SaveChanges();

            _logger.LogInformation("Updated todo with id {Id} via OData", key);

            return Updated(todo);
        }

        // DELETE: /odata/Todos(5)
        public IActionResult Delete(int key)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == key);
            if (todo == null)
            {
                _logger.LogWarning("Todo with id {Id} not found for deletion", key);
                return NotFound();
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            _logger.LogInformation("Deleted todo with id {Id} via OData", key);

            return NoContent();
        }
    }
}
