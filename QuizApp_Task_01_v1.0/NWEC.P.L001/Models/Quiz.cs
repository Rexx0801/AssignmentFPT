using System.ComponentModel.DataAnnotations;

namespace NWEC.P.L001.Models
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
    }
}
