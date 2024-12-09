using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Student_Grade_Management_System.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SystemDbContext _context;
        public ClassesController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.ToListAsync();
            if (classes == null)
            {
                return NotFound();
            }
            return View(classes);
        }
        public IActionResult Add()
        {
            // Code to add a new class
            return View();
        }
        public IActionResult Create()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}
