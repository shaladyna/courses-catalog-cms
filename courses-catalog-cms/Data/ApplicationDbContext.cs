using courses_catalog_cms.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_catalog_cms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // To będą nasze tabele w bazie danych:
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
    }
}
