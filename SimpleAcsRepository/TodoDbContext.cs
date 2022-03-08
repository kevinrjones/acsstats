using Domain;
using Microsoft.EntityFrameworkCore;

namespace SimpleTodoRepository
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
    }
}
