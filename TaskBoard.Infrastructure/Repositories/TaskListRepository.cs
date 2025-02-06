using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Data;
using TaskBoard.Infrastructure.Repositories.Interfaces;

namespace TaskBoard.Infrastructure.Repositories
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly AppDbContext _context;

        public TaskListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskList>> GetAllAsync()
        {
            return await _context.TaskLists.Include(tl => tl.Tasks).ToListAsync();
        }

        public async Task<TaskList?> GetByIdAsync(int id)
        {
            return await _context.TaskLists
                .Include(tl => tl.Tasks)
                .FirstOrDefaultAsync(tl => tl.Id == id);
        }

        public async Task AddAsync(TaskList taskList)
        {
            await _context.TaskLists.AddAsync(taskList);
        }

        public void Update(TaskList taskList)
        {
            _context.TaskLists.Update(taskList);
        }

        public void Delete(TaskList taskList)
        {
            _context.TaskLists.Remove(taskList);
        }
    }
}
