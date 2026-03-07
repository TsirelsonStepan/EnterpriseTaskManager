using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Domain;

namespace TaskManager.Api.Infrastructure.Database;

public class Context : DbContext
{
    DbSet<User> Users { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }
}