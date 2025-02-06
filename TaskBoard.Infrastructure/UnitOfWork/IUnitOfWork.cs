using TaskBoard.Infrastructure.Repositories.Interfaces;

namespace TaskBoard.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        ITaskListRepository TaskLists { get; }
        Task<int> SaveChangesAsync();
    }
}
