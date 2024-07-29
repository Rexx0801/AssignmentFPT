using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp_Task_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp_Task_02.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new QuizAppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<QuizAppDbContext>>()))
            {
                if (context.Quizzes.Any())
                {
                    return;
                }

                var quizzes = new List<Quiz>
                {
                    new Quiz { Id = Guid.NewGuid(), Title = "Math Quiz", Description = "Basic Math Quiz", Duration = 600, ThumbnailUrl = "https://example.com/math.jpg", IsActive = true },
                    new Quiz { Id = Guid.NewGuid(), Title = "Science Quiz", Description = "Basic Science Quiz", Duration = 900, ThumbnailUrl = "https://example.com/science.jpg", IsActive = true },
                    new Quiz { Id = Guid.NewGuid(), Title = "History Quiz", Description = "Basic History Quiz", Duration = 1200, ThumbnailUrl = "https://example.com/history.jpg", IsActive = true },
                    new Quiz { Id = Guid.NewGuid(), Title = "Geography Quiz", Description = "Basic Geography Quiz", Duration = 800, ThumbnailUrl = "https://example.com/geography.jpg", IsActive = true },
                    new Quiz { Id = Guid.NewGuid(), Title = "Literature Quiz", Description = "Basic Literature Quiz", Duration = 1000, ThumbnailUrl = "https://example.com/literature.jpg", IsActive = true }
                };
                context.Quizzes.AddRange(quizzes);

                var questions = new List<Question>
                {
                    new Question { Id = Guid.NewGuid(), Content = "What is 2+2?", QuestionType = "MultipleChoice", IsActive = true },
                    new Question { Id = Guid.NewGuid(), Content = "What is the capital of France?", QuestionType = "MultipleChoice", IsActive = true },
                    new Question { Id = Guid.NewGuid(), Content = "Who wrote 'To Kill a Mockingbird'?", QuestionType = "MultipleChoice", IsActive = true },
                    new Question { Id = Guid.NewGuid(), Content = "What is the boiling point of water?", QuestionType = "MultipleChoice", IsActive = true },
                    new Question { Id = Guid.NewGuid(), Content = "Who was the first president of the United States?", QuestionType = "MultipleChoice", IsActive = true }
                };
                context.Questions.AddRange(questions);

                var answers = new List<Answer>
                {
                    new Answer { Id = Guid.NewGuid(), Content = "4", IsCorrect = true, IsActive = true },
                    new Answer { Id = Guid.NewGuid(), Content = "Paris", IsCorrect = true, IsActive = true },
                    new Answer { Id = Guid.NewGuid(), Content = "Harper Lee", IsCorrect = true, IsActive = true },
                    new Answer { Id = Guid.NewGuid(), Content = "100°C", IsCorrect = true, IsActive = true },
                    new Answer { Id = Guid.NewGuid(), Content = "George Washington", IsCorrect = true, IsActive = true }
                };
                context.Answers.AddRange(answers);

                await context.SaveChangesAsync();
            }
        }
    }
}
