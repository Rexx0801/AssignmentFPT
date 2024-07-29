using System;
using System.ComponentModel.DataAnnotations;
namespace QuizApp_Task_02.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Content { get; set; }

        [Required]
        public bool IsCorrect { get; set; } = false;

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
