namespace TasksBackend.Models.Dto
{
    public class TaskDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }
        public string Completed { get; set; }
    }
}
