using courses_catalog_cms.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace courses_catalog_cms.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Pobieramy statystyki z bazy danych, żeby wysłać je do widoku
            ViewBag.CoursesCount = await _context.Courses.CountAsync();
            ViewBag.CategoriesCount = await _context.Categories.CountAsync();
            ViewBag.TrainersCount = await _context.Trainers.CountAsync();

            return View();
        }
    }
}