using Microsoft.AspNetCore.Mvc;
using Student_Grade_Management_System.Models;
using System.Diagnostics;

namespace Student_Grade_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Gauti UserType reikšmę iš sesijos
            var userType = HttpContext.Session.GetString("UserType");

            // Jei UserType nėra nustatytas (reikšmė null arba tuščia), nukreipti į prisijungimo puslapį
            if (string.IsNullOrEmpty(userType))
            {
                return RedirectToAction("Login", "Client"); // Nukreipiama į "Login" veiksmą "Client" valdiklyje
            }

            // Priskirti UserType į ViewData (jei reikia)
            ViewData["UserType"] = userType;

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
