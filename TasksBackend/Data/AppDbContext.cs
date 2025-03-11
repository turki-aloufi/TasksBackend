using Microsoft.EntityFrameworkCore;
using TasksBackend.Models;

namespace TasksBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoTask> Tasks { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTask>(entity =>
            {
                entity.HasKey(e => e.TaskId); 
            });
        }
    }
}