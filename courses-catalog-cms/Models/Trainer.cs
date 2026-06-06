using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane.")]
        [StringLength(100, ErrorMessage = "Imię i nazwisko nie może przekraczać 100 znaków.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Biogram jest wymagany.")]
        [MaxLength(1000, ErrorMessage = "Biogram jest zbyt długi (maksymalnie 1000 znaków).")]
        public string Bio { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}