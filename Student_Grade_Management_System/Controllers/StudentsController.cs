using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using System.Text;

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
			// Keep the search term in ViewData so it persists in the view
			ViewData["Search"] = search;

			// Start by fetching all students
			var students = from s in _context.Students
						   select s;

			if (HttpContext.Session.GetString("UserType") == "Parent")
			{
				students = _context.Students
				.Join(
					_context.ParentsOfStudents,
					cls => cls.Username,
					timetable => timetable.Student_Username,
					(cls, timetable) => new { Student = cls, StudentAndParent = timetable }
				)
				.Join(
					_context.Parents,
					combined => combined.StudentAndParent.Parent_Username,
					lesson => lesson.Username,
					(combined, lesson) => new { combined.Student, combined.StudentAndParent, Parent = lesson }
				)
				.Where(data => data.Parent.Username == HttpContext.Session.GetString("Username"))
				.Select(group => group.Student);
			}
			else if (HttpContext.Session.GetString("UserType") == "Teacher")
			{

			}

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

		public IActionResult Extract(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return RedirectToAction("Index", "Profile");
			}

			// Fetch the user's data
			var userProfile = _context.Students.FirstOrDefault(u => u.Username == username);
			if (userProfile == null)
			{
				return RedirectToAction("Index", "Profile");
			}

			var currentSchool = _context.Schools.Where(s => s.ID == userProfile.School_ID).Select(s => s.Name).FirstOrDefault();

			// Create the text content
			StringBuilder fileContent = new StringBuilder();
			fileContent.AppendLine("Asmeniniai duomenys");
			fileContent.AppendLine("-------------------------------------------");
			fileContent.AppendLine($"Vartotojo vardas: {userProfile.Username}");
			fileContent.AppendLine($"Slaptažodis: {userProfile.Password}");
			fileContent.AppendLine($"Vardas: {userProfile.Name}");
			fileContent.AppendLine($"Pavardė: {userProfile.Surname}");
			fileContent.AppendLine($"Gimimo data: {userProfile.Birth_Date:yyyy-MM-dd}");
			fileContent.AppendLine($"Asmens kodas: {userProfile.SSN}");
			fileContent.AppendLine($"Miestas: {userProfile.City}");
			fileContent.AppendLine($"Gatvė: {userProfile.Street}");
			fileContent.AppendLine($"Namo numeris: {userProfile.Building}");
			fileContent.AppendLine($"Buto numeris: {userProfile.Apartment}");
			fileContent.AppendLine($"Mokykla: {currentSchool}");
			fileContent.AppendLine($"Klasė: {userProfile.Class_Number} {userProfile.Class_Letter}");

			// Convert the text content to bytes
			var fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
			var fileName = $"Asmeniniai_Duomenys_{username}.txt";

			// Return the file for download
			return File(fileBytes, "text/plain", fileName);
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
			ViewBag.Schools = _context.Schools.Where(s => s.ID != student.School_ID).Select(s => new
			{
				CombinedOne = s.ID + " " + s.Name,
				s.ID,
				s.Name
			}).Distinct().ToList();

			ViewBag.CurrentSchool = _context.Schools.Where(s => s.ID == student.School_ID)
						  .Select(s => new
						  {
							  Combined = s.ID + " " + s.Name,
							  s.ID,
							  s.Name
						  })
						  .FirstOrDefault();

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

			// If there were validation errors, reload the school data and return the view with error messages
			ViewBag.Schools = _context.Schools.Select(s => new
			{
				CombinedOne = s.ID + " " + s.Name,
				Selected = s.ID == model.School_ID ? "selected" : "",
				s.ID,
				s.Name
			}).Distinct().ToList();

			ViewBag.CurrentSchool = _context.Schools.Where(s => s.ID == model.School_ID)
						  .Select(s => new
						  {
							  Combined = s.ID + " " + s.Name,
							  s.ID,
							  s.Name
						  })
						  .FirstOrDefault();


			return View(model);
		}
		public IActionResult Assign()
		{
			// Code to assign students to classes

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

		[HttpPost]
		public IActionResult Assign(string filterStudent, string filterClass)
		{
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

			var studClassCount = _context.Students
			.GroupBy(s => new { s.Class_Number, s.Class_Letter })
			.Select(g => new
			{
				Number = g.Key.Class_Number,
				Letter = g.Key.Class_Letter,
				Count = g.Count()
			})
			.Join(
				_context.Classes,
				sc => new { Number = sc.Number, Letter = sc.Letter },
				cd => new { Number = (int?)cd.Number, Letter = cd.Letter }, // Match nullable/non-nullable
				(sc, cd) => new
				{
					sc.Number,
					sc.Letter,
					sc.Count,
					MaxCount = cd.StudentCount
				}
			)
			.ToList();


			if (ModelState.IsValid)
			{

				if (!string.IsNullOrEmpty(filterStudent) || !string.IsNullOrEmpty(filterClass))
				{
					var userProfile = _context.Students.Find(filterStudent);
					if (userProfile != null)
					{

						var newProfile = userProfile;
						bool found = false;
						foreach (var classes in studClassCount)
						{
							if (classes.Number.ToString() == filterClass && classes.Count < classes.MaxCount)
							{
								newProfile.Class_Letter = classes.Letter;
								newProfile.Class_Number = classes.Number;
								found = true;
								break;
							}
						}
						if (found)
						{
							_context.Entry(userProfile).CurrentValues.SetValues(newProfile);
							_context.SaveChanges();

							ViewBag.SuccessUpdate = new
							{
								Message = $"{newProfile.Name} {newProfile.Surname} buvo pridėta(-s) prie klasės {newProfile.Class_Number} {newProfile.Class_Letter}",
								Found = true
							};
						}
						else
						{
							ViewBag.SuccessUpdate = new
							{
								Message = "Mokinio(-ės) neišėjo pridėti į norimą klasę",
								Found = false
							};
						}

						return View();
					}
				}
			}
			return View();
		}

		public IActionResult History(DateTime? filterStartDate, DateTime? filterEndDate)
		{
			// Set default values if parameters are null
			filterStartDate ??= DateTime.MinValue;  // Default to a very old date
			filterEndDate ??= DateTime.MaxValue;    // Default to a very future date

			// Filter StudentMarks
			ViewBag.StudentMarks = _context.Grades
				.Join(
					_context.Students,
					cls => cls.Student_Username,
					timetable => timetable.Username,
					(cls, timetable) => new { Grade = cls, Student = timetable }
				)
				.Join(
					_context.Lessons,
					combined => combined.Grade.Subject_ID,
					lesson => lesson.ID,
					(combined, lesson) => new { combined.Grade, combined.Student, Lesson = lesson }
				)
				.Join(
					_context.Subjects,
					combined => combined.Lesson.Subject_ID,
					lesson => lesson.ID,
					(combined, lesson) => new { combined.Grade, combined.Student, combined.Lesson, Subject = lesson }
				)
				.Where(data => data.Grade.Student_Username == HttpContext.Session.GetString("Username")
                              && data.Grade.Date >= filterStartDate
							  && data.Grade.Date <= filterEndDate)
				.Select(group => new
				{
					Grade = group.Grade.Value,
					Date = group.Grade.Date.ToString("yyyy-MM-dd"),
					Lesson = group.Subject.Name
				})
				.ToList();

			// Filter Reviews
			ViewBag.Reviews = _context.Reviews
				.Join(
					_context.Students,
					cls => cls.Student_Username,
					timetable => timetable.Username,
					(cls, timetable) => new { Review = cls, Student = timetable }
				)
				.Join(
					_context.Teachers,
					combined => combined.Review.Teacher_Username,
					teacher => teacher.Username,
					(combined, teacher) => new { combined.Review, combined.Student, Teacher = teacher }
				)
				.Join(
					_context.ReviewTypes,
					combined => combined.Review.Type,
					type => type.ID,
					(combined, type) => new { combined.Review, combined.Student, combined.Teacher, ReviewType = type }
				)
				.Where(data => data.Review.Student_Username == HttpContext.Session.GetString("Username")
                              && data.Review.Date >= filterStartDate
							  && data.Review.Date <= filterEndDate)
				.Select(group => new
				{
					Review = group.Review.Content,
					Date = group.Review.Date.ToString("yyyy-MM-dd"),
					TeacherName = group.Teacher.Name,
					TeacherSurname = group.Teacher.Surname,
					ReviewType = group.ReviewType.Name
				})
				.ToList();

			return View();
		}

		public IActionResult Delete(string username)
		{
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Students");
            }

            // Fetch the user's data
            var userProfile = _context.Students.FirstOrDefault(u => u.Username == username);
            if (userProfile == null)
            {
                return RedirectToAction("Index", "Students");
            }

            // Remove the profile from the database
            _context.Students.Remove(userProfile);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the Index page after deletion
            return RedirectToAction("Index", "Students");
        }
    }
}
