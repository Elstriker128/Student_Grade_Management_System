using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Student_Grade_Management_System.Models; // Įtraukite modelius
using System.Linq;

namespace Student_Grade_Management_System.Controllers
{
    public class ClientController : Controller
    {
        private readonly SystemDbContext _context;
        public ClientController(SystemDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        // POST: Client/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["ErrorMessage"] = "Visi laukai privalo būti užpildyti.";
                return View();
            }

            var userType = username.Substring(0, 1).ToUpper();
            var passwordHasher = new PasswordHasher<object>();

            if (userType == "S") // Studentas
            {
                var student = _context.Students.FirstOrDefault(s => s.Username == username);
                if (student != null && student.Password == password)
                {
                    // Išsaugokite studento informaciją sesijoje
                    HttpContext.Session.SetString("UserType", "Student");
                    HttpContext.Session.SetString("Username", student.Username);

                    TempData["SuccessMessage"] = "Prisijungta sėkmingai.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (userType == "T") // Mokytojas
            {
                var teacher = _context.Teachers.FirstOrDefault(t => t.Username == username);
                if (teacher != null && teacher.Password == password)
                {
                    // Išsaugokite mokytojo informaciją sesijoje
                    HttpContext.Session.SetString("UserType", "Teacher");
                    HttpContext.Session.SetString("Username", teacher.Username);

                    TempData["SuccessMessage"] = "Prisijungta sėkmingai.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (userType == "P") // Tėvas
            {
                var parent = _context.Parents.FirstOrDefault(p => p.Username == username);
                if (parent != null && parent.Password == password)
                {
                    // Išsaugokite tėvo informaciją sesijoje
                    HttpContext.Session.SetString("UserType", "Parent");
                    HttpContext.Session.SetString("Username", parent.Username);

                    TempData["SuccessMessage"] = "Prisijungta sėkmingai.";
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (userType == "A") // Administratorius
            {
                var admin = _context.Administrators.FirstOrDefault(a => a.Username == username);
                if (admin != null && admin.Password == password)
                {
                    // Išsaugokite administratoriaus informaciją sesijoje
                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetString("Username", admin.Username);

                    TempData["SuccessMessage"] = "Prisijungta sėkmingai.";
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewData["ErrorMessage"] = "Neteisingas vartotojo vardas arba slaptažodis.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Išvalome sesiją
            return RedirectToAction("Login", "Client");
        }
    }
}