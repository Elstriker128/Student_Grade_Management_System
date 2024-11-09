using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            // Code to display a form for adding a new student
            return View();
        }
        public IActionResult Extract()
        {
            // Code to extract student data
            return View();
        }
        public IActionResult Change()
        {
            // Code to preview students past
            return View();
        }
        public IActionResult Assign()
        {
            // Code to assign students to classes
            return View();
        }
    }
}
