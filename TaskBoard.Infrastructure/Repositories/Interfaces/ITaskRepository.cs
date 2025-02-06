using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<WorkTask>> GetAllAsync();
        Task<WorkTask?> GetByIdAsync(int id);
        Task AddAsync(WorkTask task);
        void Update(WorkTask task);
        void Delete(WorkTask task);
    }
}
