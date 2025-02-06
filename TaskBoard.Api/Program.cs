using Microsoft.EntityFrameworkCore;
using TaskBoard.Infrastructure.Data;
using TaskBoard.Infrastructure.UnitOfWork;
using TaskBoard.Application.Interfaces;
using TaskBoard.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// use SQLite (see appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// all services that are registered
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskListService, TaskListService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

// swagger!
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// required to use the Migrations in Docker
// anything that can go wrong?
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();  // This applies any pending migrations
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
