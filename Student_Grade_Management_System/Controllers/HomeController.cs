using DocumentFormat.OpenXml.InkML;
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

            var student = await _context.Students
                .Where(x => x.Username == userName)
                .Select(x => x.Name + " " + x.Surname)
                .FirstOrDefaultAsync();

            var teacher = await _context.Teachers
                .Where(x => x.Username == userName)
                .Select(x => x.Name + " " + x.Surname)
                .FirstOrDefaultAsync();

            var admin = await _context.Administrators
                .Where(x => x.Username == userName)
                .Select(x => x.Name + " " + x.Surname)
                .FirstOrDefaultAsync();

            var parent = await _context.Parents
                .Where(x => x.Username == userName)
                .Select(x => x.Name + " " + x.Surname)
                .FirstOrDefaultAsync();

            if (userType == "Student")
            {
                HttpContext.Session.SetString("Name", student);
            }
            else if (userType == "Teacher"){
                HttpContext.Session.SetString("Name", teacher);
            }
            else if (userType == "Admin"){
                HttpContext.Session.SetString("Name", admin);
            }
            else if (userType == "Parent"){
                HttpContext.Session.SetString("Name", parent);
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
