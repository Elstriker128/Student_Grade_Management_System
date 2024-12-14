using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Utilities;
using Student_Grade_Management_System.Models;

namespace Student_Grade_Management_System.Controllers
{
    public class TeachersController : Controller
    {
        private readonly SystemDbContext _context;

        public TeachersController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string search)
        {
            ViewData["Search"] = search;

            var teachers = from t in _context.Teachers
                           select t;

            if (!string.IsNullOrEmpty(search))
            {
                teachers = teachers.Where(t => t.Name.Contains(search) || t.Surname.Contains(search));
            }
            
            return View(await teachers.ToListAsync());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Teacher teacher)
        {
            teacher = new Teacher
            {
                Password = Request.Form["Password"],
                Name = Request.Form["Name"],
                Surname = Request.Form["Surname"],
                SSN = long.Parse(Request.Form["SSN"]),
                Email = Request.Form["Email"],
                PhoneNumber = Request.Form["PhoneNumber"],
                Street = Request.Form["Street"],
                City = Request.Form["City"],
                Building = Request.Form["Building"],
                Apartment = Request.Form["Apartment"],
                Username = "T-" + Request.Form["Name"],
            };

            var username = "T-" + teacher.Name;
            var teachers = await _context.Teachers.ToListAsync();
            int counter = 1;
            string originalUsername = username;
            while (teachers.Any(t => t.Username == username))
            {
                username = originalUsername + counter.ToString();
                counter++;
            }
            teacher.Username = username;

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult AddQualification()
        {
            return View();
        }
        public IActionResult AssignToClass()
        {
            return View();
        }

        public IActionResult ManualAssign()
        {
            return View();
        }

        public IActionResult AutoAssign()
        {
            return View();
        }
    }
}
