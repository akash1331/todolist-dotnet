using Microsoft.EntityFrameworkCore;
using SampleWebApplication1.Models;

namespace SampleWebApplication1.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some initial data
            modelBuilder.Entity<Todo>().HasData(
                new Todo 
                { 
                    Id = 1, 
                    Title = "Learn ASP.NET Core", 
                    Description = "Master building web APIs with ASP.NET Core",
                    IsCompleted = false,
                    CreatedDate = DateTime.UtcNow
                },
                new Todo 
                { 
                    Id = 2, 
                    Title = "Build Todo API", 
                    Description = "Create a RESTful API for managing todos",
                    IsCompleted = true,
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    CompletedDate = DateTime.UtcNow
                }
            );
        }
    }
}
