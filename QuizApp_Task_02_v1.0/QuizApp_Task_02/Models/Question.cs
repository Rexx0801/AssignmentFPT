using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task_02.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Content { get; set; }

        [Required]
        public string QuestionType { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ValidateNever]
        public ICollection<Answer> Answers { get; set; }
        [ValidateNever]
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
