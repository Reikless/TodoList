using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
        {

        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> User { get; set; }
    }
}
