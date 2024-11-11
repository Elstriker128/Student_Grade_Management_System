using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}