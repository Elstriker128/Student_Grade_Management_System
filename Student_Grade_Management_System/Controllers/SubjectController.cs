using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;

namespace Student_Grade_Management_System.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SystemDbContext _context;

        public SubjectController(SystemDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects.ToListAsync();
            if (subjects == null)
            {
                return NotFound();
            }
            return View(subjects);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            var newSubject = new Subject
            {
                Name = name
            };

            var subjects = await _context.Subjects.ToListAsync();
            foreach (var s in subjects)
            {
                if (s.Name == newSubject.Name)
                {
                    return View("Error", new ErrorViewModel { RequestId = "Toks dalykas jau egzistuoja." });
                }
            }

            await _context.Subjects.AddAsync(newSubject);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
