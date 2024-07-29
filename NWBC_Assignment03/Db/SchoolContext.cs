using Microsoft.EntityFrameworkCore;
using NWBC_Assignment03.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NWBC_Assignment03.Db
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Grade)
                .HasForeignKey(s => s.GradeId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));
        }
    }
}