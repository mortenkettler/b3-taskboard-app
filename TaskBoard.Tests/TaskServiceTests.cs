using TaskBoard.Application.Services;
using TaskBoard.Infrastructure.UnitOfWork;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskBoard.Tests
{
    public class TaskServiceTests
    {

        private readonly TaskService _taskService;
        private readonly AppDbContext _dbContext;
        private readonly UnitOfWork _unitOfWork;

        public TaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            // to avoid key error when seeding the database() twice
            // there are likely better ways to do it
            _dbContext.Database.EnsureDeleted();
            _unitOfWork = new UnitOfWork(_dbContext);

            _taskService = new TaskService(_unitOfWork);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var taskList = new TaskList { Id = 1, Name = "Sample List" };
            _dbContext.TaskLists.Add(taskList);
            _dbContext.Tasks.Add(new WorkTask
            {
                Id = 1,
                Name = "Sample Task",
                Description = "Test Description",
                TaskList = taskList,
                TaskListId = taskList.Id
            });
            _dbContext.SaveChanges();
        }

        [Fact]
        public void GetTaskById_Returns_CorrectTaskDTO_When_TaskExists()
        {
            var returnedTaskItem = _taskService.GetTaskById(1);
            Assert.NotNull(returnedTaskItem);
            Assert.Equal(1, returnedTaskItem.Id);
            Assert.Equal("Sample Task", returnedTaskItem.Name);
            Assert.Equal("Test Description", returnedTaskItem.Description);
            Assert.Equal(1, returnedTaskItem.TaskListId);
        }

        [Fact]
        public void GetTaskById_Returns_Null_When_TaskDoesNotExist()
        {
            var returnedTaskItem = _taskService.GetTaskById(999);
            Assert.Null(returnedTaskItem);
        }
    }
}
