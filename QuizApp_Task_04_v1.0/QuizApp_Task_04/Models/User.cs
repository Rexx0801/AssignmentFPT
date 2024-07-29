using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuizApp_Task_02.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}";

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
