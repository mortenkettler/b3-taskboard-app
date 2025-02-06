using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Data;
using TaskBoard.Infrastructure.Repositories.Interfaces;

namespace TaskBoard.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<WorkTask?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(WorkTask task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(WorkTask task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(WorkTask task)
        {
            _context.Tasks.Remove(task);
        }
    }
}
