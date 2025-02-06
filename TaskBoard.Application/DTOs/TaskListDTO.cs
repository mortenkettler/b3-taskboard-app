using System.Collections.Generic;

namespace TaskBoard.Application.DTOs
{
    public class TaskListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}
