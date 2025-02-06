using System.Collections.Generic;
using TaskBoard.Application.DTOs;

namespace TaskBoard.Application.Interfaces
{
    public interface ITaskListService
    {
        IEnumerable<TaskListDTO> GetAllTaskLists();
        TaskListDTO GetTaskListById(int id);
        TaskListDTO CreateTaskList(TaskListDTO taskListDTO);
        void UpdateTaskList(TaskListDTO taskListDTO);
        void DeleteTaskList(int id);
    }
}
