using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
namespace TaskManagerApi
{
    public class ApplicationDBContext:DbContext
    {

public ApplicationDBContext(DbContextOptions options) : base(options)
        { 
        
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskManager>().ToTable("tasks");

        }

    }
}
