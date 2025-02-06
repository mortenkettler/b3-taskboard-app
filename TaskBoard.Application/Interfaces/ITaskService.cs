using System.Collections.Generic;
using TaskBoard.Application.DTOs;

namespace TaskBoard.Application.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetAllTasks();
        TaskDTO GetTaskById(int id);
        TaskDTO CreateTask(TaskDTO taskDTO);
        void UpdateTask(TaskDTO taskDTO);
        void DeleteTask(int id);
    }
}
