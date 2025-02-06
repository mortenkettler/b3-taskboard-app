using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Repositories.Interfaces
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskList>> GetAllAsync();
        Task<TaskList?> GetByIdAsync(int id);
        Task AddAsync(TaskList taskList);
        void Update(TaskList taskList);
        void Delete(TaskList taskList);
    }
}
