using courses_catalog_cms.Data;
using courses_catalog_cms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace courses_catalog_cms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId, int page = 1)
        {
            int pageSize = 9;

            var coursesQuery = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Trainer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                coursesQuery = coursesQuery.Where(c => c.Title.Contains(searchString) || c.Description.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                coursesQuery = coursesQuery.Where(c => c.CategoryId == categoryId);
            }

            int totalItems = await coursesQuery.CountAsync();

            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var paginatedCourses = await coursesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["CurrentSearch"] = searchString;
            ViewData["CurrentCategory"] = categoryId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", categoryId);

            return View(paginatedCourses);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
