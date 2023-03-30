using Microsoft.EntityFrameworkCore;
using TasksWebApi.Domains;

namespace TasksWebApi
{
     public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options):base(options)
        {

        }
        //private string _connectionStr = "Server=.\\SQLEXPRESS;Database=Tasks;User Id=Togal;Password=1234;";
        public DbSet<Task> ToDos { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Task>()
                .Property(d => d.ExpiredDate)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionStr);
        //}
    }
}
