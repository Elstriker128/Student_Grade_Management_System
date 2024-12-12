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
            // Patikriname, ar laukai nėra tušti
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["ErrorMessage"] = "Visi laukai privalo būti užpildyti.";
                return View();
            }

            // Nustatome vartotojo tipą pagal pirmąją raidę (S - studentas, T - mokytojas, P - tėvas)
            var userType = username.Substring(0, 1).ToUpper();

            // Tiesioginis slaptažodžio tikrinimas su PasswordHasher
            var passwordHasher = new PasswordHasher<object>();

            if (userType == "S") // Studentas
            {
                var student = _context.Students.FirstOrDefault(s => s.Username == username.Substring(1));
                if (student != null)
                {
                    // Patikriname slaptažodį su PasswordHasher
                    //var result = passwordHasher.VerifyHashedPassword(null, student.Password, password);
                    var classLetter = student.Class_Letter;
                    var result = _context.Students.FirstOrDefault(s => s.Password == password);
                    //if (result == PasswordVerificationResult.Success)
                    if(result != null)
                    {
                        return RedirectToAction("Index", "Home"); // Sėkmingas prisijungimas
                    }
                }
            }
            else if (userType == "T") // Mokytojas
            {
                var teacher = _context.Teachers.FirstOrDefault(t => t.Username == username.Substring(1));
                if (teacher != null)
                {
                    // Patikriname slaptažodį su PasswordHasher
                    var result = passwordHasher.VerifyHashedPassword(null, teacher.Password, password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return RedirectToAction("Index", "Home"); // Sėkmingas prisijungimas
                    }
                }
            }
            else if (userType == "P") // Tėvas
            {
                var parent = _context.Parents.FirstOrDefault(p => p.Username == username.Substring(1));
                if (parent != null)
                {
                    // Patikriname slaptažodį su PasswordHasher
                    var result = passwordHasher.VerifyHashedPassword(null, parent.Password, password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return RedirectToAction("Index", "Home"); // Sėkmingas prisijungimas
                    }
                }
            }

            // Jei prisijungimas nepavyksta
            ViewData["ErrorMessage"] = "Neteisingas vartotojo vardas arba slaptažodis.";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}