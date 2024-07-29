using System;
using System.ComponentModel.DataAnnotations;
namespace QuizApp_Task_02.Models
{
     public class UserAnswer
        {
           public Guid Id { get; set; }

            public Guid UserId { get; set; }
            public Guid QuizId { get; set; }
            public UserQuiz UserQuiz { get; set; }

            public Guid QuestionId { get; set; }
            public Question Question { get; set; }

            public Guid AnswerId { get; set; }
            public Answer Answer { get; set; }

            [Required]
            public bool IsCorrect { get; set; }
    }
}
