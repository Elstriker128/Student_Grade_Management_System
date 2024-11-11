using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            // Code to display a form for adding a new subject
            return View();
        }
        public IActionResult Remove()
        {
            // Code to remove a subject
            return View();
        }
    }
}
