using System.Collections.Generic;
using System.Linq;
using TaskBoard.Application.DTOs;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.UnitOfWork;

namespace TaskBoard.Application.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<TaskListDTO> GetAllTaskLists()
        {
            var taskLists = _unitOfWork.TaskLists.GetAllAsync().Result;
            return taskLists.Select(tl => new TaskListDTO
            {
                Id = tl.Id,
                Name = tl.Name,
                Tasks = tl.Tasks.Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    TaskListId = t.TaskListId
                }).ToList()
            });
        }

        public TaskListDTO GetTaskListById(int id)
        {
            var taskList = _unitOfWork.TaskLists.GetByIdAsync(id).Result;
            if (taskList == null)
            {
                return null;
            }
            return new TaskListDTO
            {
                Id = taskList.Id,
                Name = taskList.Name,
                Tasks = taskList.Tasks.Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    TaskListId = t.TaskListId
                }).ToList()
            };
        }

        public TaskListDTO CreateTaskList(TaskListDTO taskListDTO)
        {
            // Create a new TaskList and set its name.
            var newTaskList = new TaskList { Name = taskListDTO.Name };

            // if wished, we can add tasks, but only name is required
            if (taskListDTO.Tasks != null && taskListDTO.Tasks.Any())
            {
                foreach (var taskDto in taskListDTO.Tasks)
                {
                    var newTask = new WorkTask
                    {
                        Name = taskDto.Name,
                        Description = taskDto.Description,
                        TaskListId = newTaskList.Id,
                        TaskList = newTaskList,
                    };
                    newTaskList.Tasks.Add(newTask);
                }
            }

            _unitOfWork.TaskLists.AddAsync(newTaskList).Wait();
            _unitOfWork.SaveChangesAsync().Wait();

            return new TaskListDTO
            {
                Id = newTaskList.Id,
                Name = newTaskList.Name,
                Tasks = newTaskList.Tasks.Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    TaskListId = t.TaskListId
                }).ToList()
            };
        }

        public void UpdateTaskList(TaskListDTO taskListDTO)
        {
            // Retrieve the existing TaskList.
            var existingTaskList = _unitOfWork.TaskLists.GetByIdAsync(taskListDTO.Id).Result;
            if (existingTaskList == null)
            {
                return; // better: throw an exception.
            }

            existingTaskList.Name = taskListDTO.Name;
            _unitOfWork.TaskLists.Update(existingTaskList);
            _unitOfWork.SaveChangesAsync().Wait();
        }

        public void DeleteTaskList(int id)
        {
            var existingTaskList = _unitOfWork.TaskLists.GetByIdAsync(id).Result;
            if (existingTaskList == null)
            {
                return; // better to throw an exception!
            }

            _unitOfWork.TaskLists.Delete(existingTaskList);
            _unitOfWork.SaveChangesAsync().Wait();
        }
    }
}
