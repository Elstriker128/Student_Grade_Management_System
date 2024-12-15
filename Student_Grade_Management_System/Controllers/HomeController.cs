using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using System.Diagnostics;

namespace Student_Grade_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly SystemDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SystemDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Gauti UserType reikšmę iš sesijos
            var userType = HttpContext.Session.GetString("UserType");
            var userName = HttpContext.Session.GetString("Username");

            // Jei UserType nėra nustatytas (reikšmė null arba tuščia), nukreipti į prisijungimo puslapį
            if (string.IsNullOrEmpty(userType))
            {
                return RedirectToAction("Login", "Client");
            }

            // Priskirti UserType į ViewData (jei reikia)
            TempData["UserType"] = userType;
            TempData["Username"] = userName;

            if (userType == "Parent")
            {
                var queryKids = _context.ParentsOfStudents.AsQueryable();
                queryKids = queryKids.Where(i => i.Parent_Username == userName);

                var kids = await queryKids
                    .Select(i => new
                    {
                        i.Student_Username,
                        name = _context.Students
                            .Where(j => j.Username == i.Student_Username)
                            .Select(j => j.Name + " " + j.Surname)
                            .FirstOrDefault()
                    })
                    .ToListAsync();
                    foreach(var kid in kids)
                    {
                        Console.WriteLine(kid.name);
                    }
                var kidsList = kids.Select(k => new { Username = k.Student_Username, FullName = k.name }).ToList();
                ViewData["Kids"] = new SelectList(kidsList, "Username", "FullName");
            }

            var student = await _context.Students
                .Where(x => x.Username == userName)
                .Select(x => x.Name + " " + x.Surname)
                .FirstOrDefaultAsync();

            if (userType == "Student")
            {
                HttpContext.Session.SetString("Name", student);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Schedule()
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
