using Microsoft.EntityFrameworkCore;

using TaskManager.Api.Domain.Entity;

namespace TaskManager.Api.Infrastructure;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) {}

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
}
