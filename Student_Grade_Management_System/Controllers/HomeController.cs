using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Student_Grade_Management_System.Models;
using System.Diagnostics;

namespace Student_Grade_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SystemDbContext _context;

        public HomeController(ILogger<HomeController> logger, SystemDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get user type from session
            var userType = HttpContext.Session.GetString("UserType");
            ViewData["UserType"] = userType;

            // Get username from session
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                // Attempt to find the student by username
                var student = _context.Students.FirstOrDefault(s => s.Username == username);
                if (student != null)
                {
                    HttpContext.Session.SetString("UserNameAndSurname", $"{student.Name} {student.Surname}");
                }
                //else
                //{
                //    _logger.LogWarning("No student found with username: {Username}", username);
                //    ViewData["UserNameSurname"] = "";
                //}
            }
            //else
            //{
            //    _logger.LogWarning("Session does not contain 'Username'");
            //    ViewData["UserNameSurname"] = "No username in session";
            //}

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
