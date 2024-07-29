using Microsoft.EntityFrameworkCore;
using NWBC_Assignment02.Models;

namespace NWBC_Assignment02.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }
    }
}