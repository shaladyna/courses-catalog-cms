using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł kursu jest wymagany.")]
        [StringLength(100, ErrorMessage = "Tytuł nie może być dłuższy niż 100 znaków.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis kursu jest wymagany.")]
        [MaxLength(4000, ErrorMessage = "Opis jest zbyt długi (maksymalnie 4000 znaków).")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cena jest wymagana.")]
        [Range(0, 99999.99, ErrorMessage = "Cena musi mieścić się w przedziale od 0 do 99 999,99 zł.")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        // Klucze obce
        [Required(ErrorMessage = "Wybór kategorii jest wymagany.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Wybór prowadzącego jest wymagany.")]
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
    }
}