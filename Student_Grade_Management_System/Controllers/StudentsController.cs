using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;

namespace Student_Grade_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SystemDbContext _context;
        public StudentsController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string search)
        {
            // Store the search term in ViewData so it persists in the view
            ViewData["Search"] = search;

            // Start by fetching all students
            var students = from s in _context.Students
                           select s;

            // If there is a search query, filter students by Name or Surname
            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.Name.Contains(search) || s.Surname.Contains(search));
            }

            // Execute the query and return the results to the view
            return View(await students.ToListAsync());
        }
        // GET: Students/Add
        public IActionResult Add()
        {
            // Populate dropdown for Schools
            ViewBag.Schools = _context.Schools
                .Select(s => new
                {
                    CombinedOne = s.ID + " " + s.Name,
                    s.ID,
                    s.Name
                })
                .Distinct()
                .ToList();

            return View();
        }

        // POST: Students/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Student model, string combinedSchool)
        {
            if (ModelState.IsValid)
            {

                var usernameExists = _context.Students.Any(s => s.Username == model.Username);
                if (usernameExists)
                {
                    // Add a model error and re-render the form
                    ModelState.AddModelError("Username", "Šis vartotojo vardas jau užimtas. Prašau parinkti kitą.");

                    // Repopulate the dropdowns for Schools
                    ViewBag.Schools = _context.Schools
                        .Select(s => new
                        {
                            CombinedOne = s.ID + " " + s.Name,
                            s.ID,
                            s.Name
                        })
                        .Distinct()
                        .ToList();

                    return View(model); // Return the form with validation errors
                }

                // Process the combined dropdown values for School_ID and Class
                if (!string.IsNullOrEmpty(combinedSchool))
                {
                    // Extract and assign the School_ID from combinedSchool
                    var schoolParts = combinedSchool.Split(" "); // Adjust this logic to split the value as required
                    model.School_ID = int.Parse(schoolParts[0]); // Assuming the first part is the School_ID
                }

                // Add the student to the database
                _context.Students.Add(model);
                _context.SaveChanges();

                // Redirect to the Index or another page after successful form submission
                return RedirectToAction("Index"); // You can redirect to another page, like a success page
            }

            // If the model is invalid, repopulate the dropdowns and return the view
            ViewBag.Schools = _context.Schools
                .Select(s => new
                {
                    CombinedOne = s.ID + " " + s.Name,
                    s.ID,
                    s.Name
                })
                .Distinct()
                .ToList();

            return View(model); // Return to the form with validation errors
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
        public IActionResult History()
        {
            // Code to assign students to classes
            return View();
        }
        public IActionResult SpecificHistory()
        {
            // Code to assign students to classes
            return View();
        }
    }
}
