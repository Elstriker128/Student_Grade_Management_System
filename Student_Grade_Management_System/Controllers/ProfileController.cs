using Microsoft.AspNetCore.Mvc;

namespace Student_Grade_Management_System.Controllers
{
    public class ProfileController : Controller
    {
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

            // Get the user profile based on the username
            var userProfile = _context.Students.Find(username); // Replace _userService with your data fetching logic

            if (userProfile == null)
            {
                return NotFound("Profile not found.");
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
