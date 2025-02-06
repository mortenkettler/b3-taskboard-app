using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        // EF core representations of the two tables
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // user story 6: ensure cascading delete!
            modelBuilder.Entity<TaskList>()
                .HasMany(tl => tl.Tasks)
                .WithOne(t => t.TaskList)
                .HasForeignKey(t => t.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
