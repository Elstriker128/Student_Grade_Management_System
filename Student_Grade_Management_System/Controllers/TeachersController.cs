using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            // Code to display a form for adding a new teacher
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult AddQualification()
        {
            return View();
        }

        public IActionResult Find()
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
        public IActionResult Profile()
        {
            return View();
        }
    }
}
