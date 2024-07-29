using NUnit.Framework;
using Microsoft.Extensions.Logging;
using QuizApp.Services;
using Microsoft.EntityFrameworkCore;
using QuizApp_Task_02.Models;
using QuizApp_Task_04.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

namespace QuizApp.Tests
{
    public class QuizServiceTests
    {
        private QuizService _quizService;
        private QuizAppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<QuizAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new QuizAppDbContext(options);

            SeedDatabase();

            _quizService = new QuizService(_context, new Mock<ILogger<QuizService>>().Object);
        }

        private void SeedDatabase()
        {
            _context.Quizzes.AddRange(new List<Quiz>
            {
                new Quiz { Id = Guid.NewGuid(), Title = "Sample Quiz", Description = "Description", Duration = 60,
                    ThumbnailUrl = "thumbnail.png"}
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task TestPrepareQuizForUserAsync()
        {
            var prepareQuizViewModel = new PrepareQuizViewModel
            {
                QuizId = _context.Quizzes.First().Id,
                UserId = Guid.NewGuid(),
                QuizCode = "SQZ123"
            };

            var result = await _quizService.PrepareQuizForUserAsync(prepareQuizViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.NewGuid(), result.Id);
        }

        [Test]
        public async Task TestTakeQuizAsync()
        {
            var takeQuizViewModel = new TakeQuizViewModel
            {
                QuizId = _context.Quizzes.First().Id,
                UserId = Guid.NewGuid()
            };

            var result = await _quizService.TakeQuizAsync(takeQuizViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.NewGuid(), result.Id);
        }

        [Test]
        public async Task TestSubmitQuizAsync()
        {
            var submitQuizViewModel = new QuizSubmissionViewModel
            {
                QuizId = _context.Quizzes.First().Id,
                UserId = Guid.NewGuid(),
                UserAnswers = new List<UserAnswerSubmissionViewModel>
                {
                    new UserAnswerSubmissionViewModel { QuestionId = 1, AnswerId = 1 }
                }
            };

            var result = await _quizService.SubmitQuizAsync(submitQuizViewModel);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestGetQuizResultAsync()
        {
            var getQuizResultViewModel = new GetQuizResultViewModel
            {
                QuizId = _context.Quizzes.First().Id,
                UserId = Guid.NewGuid()
            };

            var result = await _quizService.GetQuizResultAsync(getQuizResultViewModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(_context.Quizzes.First().Id, result.QuizId);
        }
    }
}
