using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class ClassesController : Controller
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
        public IActionResult Create()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}
