using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }

        // Relacja: Jedna kategoria ma wiele kursów
        public List<Course>? Courses { get; set; }
    }
}