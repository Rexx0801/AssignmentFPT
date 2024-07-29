using Microsoft.EntityFrameworkCore;
namespace QuizApp_Task_02.Models
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User and Role
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // User and Quiz
            modelBuilder.Entity<UserQuiz>()
                .HasKey(uq => new { uq.UserId, uq.QuizId });

            modelBuilder.Entity<UserQuiz>()
                .HasOne(uq => uq.User)
                .WithMany(u => u.UserQuizzes)
                .HasForeignKey(uq => uq.UserId);

            modelBuilder.Entity<UserQuiz>()
                .HasOne(uq => uq.Quiz)
                .WithMany(q => q.UserQuizzes)
                .HasForeignKey(uq => uq.QuizId);

            // UserQuiz and UserAnswer
            modelBuilder.Entity<UserAnswer>()
                .HasKey(ua => new { ua.UserId, ua.QuizId, ua.QuestionId, ua.AnswerId });

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.UserQuiz)
                .WithMany(uq => uq.UserAnswers)
                .HasForeignKey(ua => new { ua.UserId, ua.QuizId })
                .OnDelete(DeleteBehavior.Restrict);

            // Question and UserAnswer
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer and UserAnswer
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quiz and Question
            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Question and Answer
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
