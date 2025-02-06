using System.Collections.Generic;
using System.Linq;
using TaskBoard.Application.DTOs;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.UnitOfWork;

namespace TaskBoard.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var tasks = _unitOfWork.Tasks.GetAllAsync().Result;
            return tasks.Select(t => new TaskDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                TaskListId = t.TaskListId
            });
        }

        public TaskDTO GetTaskById(int id)
        {
            var task = _unitOfWork.Tasks.GetByIdAsync(id).Result;
            if (task == null)
            {
                return null;
            }
            return new TaskDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskListId = task.TaskListId
            };
        }

        public TaskDTO CreateTask(TaskDTO taskDTO)
        {
            var taskList = _unitOfWork.TaskLists.GetByIdAsync(taskDTO.TaskListId).Result;
            if (taskList == null)
            {
                throw new System.Exception("TaskList not found.");
            }

            // map DTO to new Task domain entity.
            var newTask = new WorkTask
            {
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                TaskList = taskList,
                TaskListId = taskDTO.TaskListId
            };

            _unitOfWork.Tasks.AddAsync(newTask).Wait();
            _unitOfWork.SaveChangesAsync().Wait();

            return new TaskDTO
            {
                Id = newTask.Id,
                Name = newTask.Name,
                Description = newTask.Description,
                TaskListId = newTask.TaskListId
            };
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            var existingTask = _unitOfWork.Tasks.GetByIdAsync(taskDTO.Id).Result;
            if (existingTask == null)
            {
                throw new System.Exception("Task not found.");
            }

            existingTask.Name = taskDTO.Name;
            existingTask.Description = taskDTO.Description;
            _unitOfWork.Tasks.Update(existingTask);
            _unitOfWork.SaveChangesAsync().Wait();
        }

        public void DeleteTask(int id)
        {
            var existingTask = _unitOfWork.Tasks.GetByIdAsync(id).Result;
            if (existingTask == null)
            {
                throw new System.Exception("Task not found.");
            }

            _unitOfWork.Tasks.Delete(existingTask);
            _unitOfWork.SaveChangesAsync().Wait();
        }
    }
}
