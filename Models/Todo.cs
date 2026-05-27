namespace SampleWebApplication1.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedDate { get; set; }
    }
}
