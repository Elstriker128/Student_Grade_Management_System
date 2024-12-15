using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;



namespace Student_Grade_Management_System.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SystemDbContext _context;
        public ProfileController(SystemDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index");
            }

            // Fetch the user profile based on the username

            var userProfile = _context.Students.Find(username); // Replace _userService with your data fetching logic

            if (userProfile == null)
            {
                return NotFound("Profile not found.");
            }
            
            var userType = HttpContext.Session.GetString("UserType");

            if(userType == "Parent"){
                var name = _context.Students.Where(i => i.Username == username).Select(i => i.Name + " " + i.Surname).FirstOrDefault();
                HttpContext.Session.SetString("Kid", username);
                HttpContext.Session.SetString("KidName", name);
            }

            // Pass the profile data to the view
            ViewBag.CurrentSchool = _context.Schools.Where(s => s.ID == userProfile.School_ID).Select(s => new {
                s.Name
            }).FirstOrDefault();

            return View(userProfile);

        }
        public IActionResult TProfile()
        {
            return View();
        }
    }
}
