
using Microsoft.EntityFrameworkCore;
using NWEC.P.L001_Task3.DataAccessLayer.Models;

public class QuizAppDbContext : DbContext
{
    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options) { }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserQuiz> UserQuizzes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserQuiz>().HasKey(uq => new { uq.UserId, uq.QuizId });
        modelBuilder.Entity<QuizQuestion>().HasKey(qq => new { qq.QuizId, qq.QuestionId });
        modelBuilder.Entity<UserAnswer>().HasKey(ua => new { ua.UserId, ua.QuestionId, ua.AnswerId });
    }
}

