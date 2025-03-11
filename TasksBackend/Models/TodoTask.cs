using System.ComponentModel.DataAnnotations;

namespace TasksBackend.Models
{
    public class TodoTask 
    {
        [Key]
        public string TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public bool Completed { get; set; } = false;
    }
}