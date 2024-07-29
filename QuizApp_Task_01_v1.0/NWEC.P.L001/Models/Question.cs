using System.ComponentModel.DataAnnotations;

namespace NWEC.P.L001.Models
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
    }
}
