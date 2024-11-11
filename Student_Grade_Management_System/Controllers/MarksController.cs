using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class MarksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            // Code to add a new class
            return View();
        }
        public IActionResult ClassMarks()
        {
            // Code to create a new schedule for a class
            return View();
        }
        public IActionResult Graphic()
        {
            // Code to add a new class
            return View();
        }
    }
}
