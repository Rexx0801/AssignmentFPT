using System.ComponentModel.DataAnnotations;

namespace NWEC.P.L001.Models
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
    }
}
