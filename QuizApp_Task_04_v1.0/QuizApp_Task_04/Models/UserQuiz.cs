using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task_02.Models
{
    public class UserQuiz
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

        [Required]
        public string QuizCode { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
