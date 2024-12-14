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
		public IActionResult Change(string username)
		{
			// Fetch the student from the database by ID
			var student = _context.Students.Find(username); // Adjust this for your context
			if (student == null)
			{
				return RedirectToAction("Index");
			}

			// Load schools into ViewBag for the dropdown
			ViewBag.Schools = _context.Schools.Where(s => s.ID != student.School_ID).Select(s => new {
				CombinedOne = s.ID + " " + s.Name,
				s.ID,
				s.Name
			}).Distinct().ToList();

			ViewBag.CurrentSchool = _context.Schools.Where(s => s.ID == student.School_ID)
						  .Select(s => new {
							  Combined = s.ID + " " + s.Name,
							  s.ID,
							  s.Name
						  })
						  .FirstOrDefault();

			//student.Birth_Date = DateOnly.Parse(student.Birth_Date.ToString("yyyy-MM-dd"));

			return View(student);
		}

		[HttpPost]
		public IActionResult Change(Student model, string combinedSchool)
		{
			if (ModelState.IsValid)
			{
				// Fetch the existing student by username
				var existingStudent = _context.Students.Find(model.Username);
				if (existingStudent != null)
				{
					// If the password field is not empty, update the password
					if (model.Password == "defaultPassword")
					{
						model.Password = existingStudent.Password;  // Update with new password
					}

					// Ensure that other fields are updated as required
					if (!string.IsNullOrEmpty(combinedSchool))
					{
						var schoolParts = combinedSchool.Split(" ");
						model.School_ID = int.Parse(schoolParts[0]);
					}

					// Update other fields and mark the entry as modified
					_context.Entry(existingStudent).CurrentValues.SetValues(model);
					_context.SaveChanges();
					return RedirectToAction("Index"); // Redirect to index after successful update
				}
			}

			//foreach (var key in ModelState.Keys)
			//{
			//	var errors = ModelState[key].Errors;
			//	foreach (var error in errors)
			//	{
			//		// Log or inspect the error message
			//		Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
			//	}
			//}



			// If there were validation errors, reload the school data and return the view with error messages
			ViewBag.Schools = _context.Schools.Select(s => new {
				CombinedOne = s.ID + " " + s.Name,
				Selected = s.ID == model.School_ID ? "selected" : "",
				s.ID,
				s.Name
			}).Distinct().ToList();

			ViewBag.CurrentSchool = _context.Schools.Where(s => s.ID == model.School_ID)
						  .Select(s => new {
							  Combined = s.ID + " " + s.Name,
							  s.ID,
							  s.Name
						  })
						  .FirstOrDefault();

			//model.Birth_Date = DateTime.Parse(model.Birth_Date.Date.ToString("MM/dd/yyyy"));

			return View(model);
		}
		public IActionResult Assign()
		{

			// Used to assign students to classes

			ViewBag.Students = _context.Students
				.Select(s => new
				{
					CombinedName = s.Name + " " + s.Surname,
					s.Name,
					s.Surname,
					s.Username
				})
				.Distinct()
				.ToList();

			ViewBag.Classes = _context.Classes.Select(s => s.Number).Distinct().ToList();

			ViewBag.SuccessUpdate = new
			{
				Message = "",
				Found = false
			};

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
