using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [Display(Name = "Tytuł kursu")]
        public string Title { get; set; }

        [Display(Name = "Opis kursu")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Cena (PLN)")]
        public decimal Price { get; set; }

        [Display(Name = "Miniaturka (URL)")]
        public string? ImageUrl { get; set; }

        // Klucze obce
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Prowadzący")]
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
    }
}