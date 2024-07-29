using Microsoft.EntityFrameworkCore;
using NWBC_Assignment03.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace NWBC_Assignment03.DbContext
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Grade-Student: One-to-Many
            modelBuilder.Entity<Grade>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Grade)
                .HasForeignKey(s => s.GradeId);

            // Student-Course: Many-to-Many
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));
        }
    }
}