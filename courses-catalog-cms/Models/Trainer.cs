using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane")]
        [Display(Name = "Imię i nazwisko")]
        public string FullName { get; set; }

        [Display(Name = "Biogram")]
        public string? Bio { get; set; }

        [Display(Name = "Zdjęcie (URL)")]
        public string? ImageUrl { get; set; }

        // Relacja: Jeden trener prowadzi wiele kursów
        public List<Course>? Courses { get; set; }
    }
}