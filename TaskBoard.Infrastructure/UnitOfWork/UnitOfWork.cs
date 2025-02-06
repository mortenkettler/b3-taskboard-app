using TaskBoard.Infrastructure.Data;
using TaskBoard.Infrastructure.Repositories;
using TaskBoard.Infrastructure.Repositories.Interfaces;

namespace TaskBoard.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ITaskRepository? _tasks;
        private ITaskListRepository? _taskLists;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITaskRepository Tasks => _tasks ??= new TaskRepository(_context);

        public ITaskListRepository TaskLists => _taskLists ??= new TaskListRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
