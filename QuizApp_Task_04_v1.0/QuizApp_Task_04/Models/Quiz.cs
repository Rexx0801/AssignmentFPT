using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuizApp_Task_02.Models
{
    public class Quiz
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 3600)]
        public int Duration { get; set; }

        [StringLength(500)]
        public string ThumbnailUrl { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ValidateNever]
        public ICollection<Question> Questions { get; set; }

        [ValidateNever]
        public ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
