using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;

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

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Teachers = _context.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int classNumber, int studentCount, string Teacher_Username)
        {
            var newClass = new Class
            {
                Number = classNumber,
                StudentCount = studentCount,
                Teacher_Username = Teacher_Username
            };

            var classes = await _context.Classes.ToListAsync();
            foreach (var c in classes)
            {
                if (c.Number == classNumber)
                {
                    char lastLetter = c.Letter.Last();
                    newClass.Letter = ((char)(lastLetter + 1)).ToString();
                }
            }

            await _context.Classes.AddAsync(newClass);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public async Task<IActionResult> AutoAssign()
        {
            var freeClasses = await _context.Classes
                .Where(c => c.Teacher_Username == null)
                .ToListAsync();

            var freeTeachers = await _context.Teachers
                .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
                .ToListAsync();

            ViewBag.FreeClasses = freeClasses;
            ViewBag.FreeTeachers = freeTeachers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AutoAssignConfirm()
        {
            var freeClasses = await _context.Classes
                .Where(c => c.Teacher_Username == null)
                .ToListAsync();

            var freeTeachers = await _context.Teachers
                .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
                .ToListAsync();

            int teacherIndex = 0;
            foreach (var freeClass in freeClasses)
            {
                if (teacherIndex >= freeTeachers.Count)
                {
                    break;
                }

                freeClass.Teacher_Username = freeTeachers[teacherIndex].Username;
                teacherIndex++;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}
