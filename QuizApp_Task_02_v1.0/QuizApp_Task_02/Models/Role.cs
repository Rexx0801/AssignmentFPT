using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuizApp_Task_02.Models
{
    public class Role : IdentityRole<Guid>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
