using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Change()
        {
            // Code to create a new schedule for a class
            return View();
        }
        public IActionResult Comment()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}