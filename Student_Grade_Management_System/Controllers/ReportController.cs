using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Export()
        {
            return View();
        }
        public IActionResult Report()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}