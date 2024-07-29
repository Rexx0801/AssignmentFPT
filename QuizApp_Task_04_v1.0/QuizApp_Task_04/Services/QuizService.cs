using Microsoft.EntityFrameworkCore;
using QuizApp_Task_02.Models;
using QuizApp_Task_04.ViewModel;

namespace QuizApp.Services
{
    public class QuizService
    {
        private readonly QuizAppDbContext _context;
        private readonly ILogger<QuizService> _logger;

        public QuizService(QuizAppDbContext context, ILogger<QuizService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<QuizPrepareInfoViewModel?> PrepareQuizForUserAsync(PrepareQuizViewModel prepareQuizViewModel)
        {
            try
            {
                var quiz = await _context.Quizzes
                    .Where(q => q.Id == prepareQuizViewModel.QuizId)
                    .Select(q => new QuizPrepareInfoViewModel
                    {
                        Id = q.Id,
                        Title = q.Title,
                        Description = q.Description,
                        Duration = q.Duration,
                        ThumbnailUrl = q.ThumbnailUrl,
                        User = new UserViewModel
                        {
                            Id = prepareQuizViewModel.UserId,
                        }
                    })
                    .FirstOrDefaultAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing quiz for user.");
                return null;
            }
        }

        public async Task<QuizForTestViewModel> TakeQuizAsync(TakeQuizViewModel model)
        {
            try
            {
                var quiz = await _context.Quizzes
                    .Include(q => q.Questions)
                        .ThenInclude(q => q.Answers)
                    .Where(q => q.Id == model.QuizId)
                    .Select(q => new QuizForTestViewModel
                    {
                        Id = q.Id,
                        Title = q.Title,
                        Description = q.Description,
                        StartTime = DateTime.UtcNow,
                        Duration = q.Duration,
                        Questions = q.Questions.Select(qs => new QuestionForTestViewModel
                        {
                            Id = qs.Id,
                            Content = qs.Content,
                            QuestionType = qs.QuestionType,
                            Answers = qs.Answers.Select(a => new AnswerForTestViewModel
                            {
                                Id = a.Id,
                                Content = a.Content
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                return quiz;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quiz for test.");
                throw;
            }
        }

        public async Task<bool> SubmitQuizAsync(QuizSubmissionViewModel model)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting quiz.");
                return false;
            }
        }

        public async Task<QuizResultViewModel> GetQuizResultAsync(GetQuizResultViewModel model)
        {
            try
            {
                var result = new QuizResultViewModel
                {
                    QuizId = model.QuizId,
                    UserId = model.UserId,
                    CorrectAnswers = 0,
                    TotalQuestions = 10,
                    Score = 75
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving quiz result.");
                throw;
            }
        }
    }
}
