using System.Collections.Generic;

namespace TaskBoard.Domain.Entities
{
    public class TaskList
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();

        public TaskList() { }

        public TaskList(string name)
        {
            Name = name;
        }
    }
}
