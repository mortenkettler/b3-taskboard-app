namespace TaskBoard.Application.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int TaskListId { get; set; }
    }
}
