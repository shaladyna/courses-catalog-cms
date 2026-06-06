using System.ComponentModel.DataAnnotations;

namespace courses_catalog_cms.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [StringLength(50, ErrorMessage = "Nazwa kategorii nie może przekraczać 50 znaków.")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}