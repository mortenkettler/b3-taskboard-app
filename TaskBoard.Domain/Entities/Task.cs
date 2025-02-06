namespace TaskBoard.Domain.Entities
{
    public class WorkTask
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public int TaskListId { get; set; }
        public required TaskList TaskList { get; set; }

        public WorkTask() { }

        public WorkTask(string name, string description, TaskList taskList)
        {
            Name = name;
            Description = description;
            TaskList = taskList;
        }
    }
}
