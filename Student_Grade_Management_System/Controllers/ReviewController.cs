using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Student_Grade_Management_System.Controllers
{
    public class ReviewController : Controller
    {
        private readonly SystemDbContext _context;

        public ReviewController(SystemDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userType = HttpContext.Session.GetString("UserType");
            var userName = HttpContext.Session.GetString("Username");
            List<ReviewM>? reviews = null;

            if (userType == "Student")
            {
                reviews = await _context.Reviews
                                                .Where(r => r.Student_Username == userName)
                                                .Select(r => new ReviewM
                                                {
                                                    ID = r.ID,
                                                    Date = r.Date,
                                                    Content = r.Content,
                                                    // Užkrauname tipo pavadinimą tiesiogiai iš ReviewType lentelės
                                                    Type = _context.ReviewTypes
                                                                    .Where(t => t.ID == r.Type)  // Palyginame ReviewType ID su Review Type
                                                                    .Select(t => t.Name)         // Grąžiname pavadinimą
                                                                    .FirstOrDefault(),
                                                    Student_Name = _context.Students
                                                                        .Where(m => m.Username == r.Student_Username) // Palyginame su Student_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname studento vardą
                                                                        .FirstOrDefault(),
                                                    // Užkrauname mokytojo vardą pagal Teacher_Username
                                                    Teacher_Name = _context.Teachers
                                                                        .Where(m => m.Username == r.Teacher_Username) // Palyginame su Teacher_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname mokytojo vardą
                                                                        .FirstOrDefault()
                                                })
                                                .ToListAsync();
            }
            else if (userType == "Teacher"){
                reviews = await _context.Reviews
                                                .Where(r => r.Teacher_Username == userName)
                                                .Select(r => new ReviewM
                                                {
                                                    ID = r.ID,
                                                    Date = r.Date,
                                                    Content = r.Content,
                                                    // Užkrauname tipo pavadinimą tiesiogiai iš ReviewType lentelės
                                                    Type = _context.ReviewTypes
                                                                    .Where(t => t.ID == r.Type)  // Palyginame ReviewType ID su Review Type
                                                                    .Select(t => t.Name)         // Grąžiname pavadinimą
                                                                    .FirstOrDefault(),
                                                    Student_Name = _context.Students
                                                                        .Where(m => m.Username == r.Student_Username) // Palyginame su Student_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname studento vardą
                                                                        .FirstOrDefault(),
                                                    // Užkrauname mokytojo vardą pagal Teacher_Username
                                                    Teacher_Name = _context.Teachers
                                                                        .Where(m => m.Username == r.Teacher_Username) // Palyginame su Teacher_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname mokytojo vardą
                                                                        .FirstOrDefault()
                                                })
                                                .ToListAsync();
            }
            else if(userType == "Admin"){
                reviews = await _context.Reviews
                                                .Select(r => new ReviewM
                                                {
                                                    ID = r.ID,
                                                    Date = r.Date,
                                                    Content = r.Content,
                                                    // Užkrauname tipo pavadinimą tiesiogiai iš ReviewType lentelės
                                                    Type = _context.ReviewTypes
                                                                    .Where(t => t.ID == r.Type)  // Palyginame ReviewType ID su Review Type
                                                                    .Select(t => t.Name)         // Grąžiname pavadinimą
                                                                    .FirstOrDefault(),
                                                    Student_Name = _context.Students
                                                                        .Where(m => m.Username == r.Student_Username) // Palyginame su Student_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname studento vardą
                                                                        .FirstOrDefault(),
                                                    // Užkrauname mokytojo vardą pagal Teacher_Username
                                                    Teacher_Name = _context.Teachers
                                                                        .Where(m => m.Username == r.Teacher_Username) // Palyginame su Teacher_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname mokytojo vardą
                                                                        .FirstOrDefault()
                                                })
                                                .ToListAsync();
            }
            else if(userType == "Parent"){
                var kidsUsernames = _context.ParentsOfStudents
                            .Where(m => m.Parent_Username == userName)
                            .Select(m => m.Student_Username)
                            .ToList();
                reviews = await _context.Reviews
                                                .Where(r => kidsUsernames.Contains(r.Student_Username))
                                                .Select(r => new ReviewM
                                                {
                                                    ID = r.ID,
                                                    Date = r.Date,
                                                    Content = r.Content,
                                                    // Užkrauname tipo pavadinimą tiesiogiai iš ReviewType lentelės
                                                    Type = _context.ReviewTypes
                                                                    .Where(t => t.ID == r.Type)  // Palyginame ReviewType ID su Review Type
                                                                    .Select(t => t.Name)         // Grąžiname pavadinimą
                                                                    .FirstOrDefault(),
                                                    Student_Name = _context.Students
                                                                        .Where(m => m.Username == r.Student_Username) // Palyginame su Student_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname studento vardą
                                                                        .FirstOrDefault(),
                                                    // Užkrauname mokytojo vardą pagal Teacher_Username
                                                    Teacher_Name = _context.Teachers
                                                                        .Where(m => m.Username == r.Teacher_Username) // Palyginame su Teacher_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname mokytojo vardą
                                                                        .FirstOrDefault()
                                                })
                                                .ToListAsync();
            }
            else
            {
                // Galite apdoroti neteisingą "userType" reikšmę
                return RedirectToAction("Error", "Home");
            }
            if (reviews == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            var teachers = await _context.Teachers
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            ViewData["Teachers"] = new SelectList(teachers, "Username", "FullName");
            
            var reviewTypes = _context.ReviewTypes.ToList();
            ViewData["ReviewTypes"] = new SelectList(reviewTypes, "ID", "Name");

            TempData["UserType"] = userType;
            TempData["Username"] = userName;

            return View(reviews);
        }

        public async Task<IActionResult> Add()
        {
            var userType = HttpContext.Session.GetString("UserType");
            var userName = HttpContext.Session.GetString("Username");
            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            if (userType == "Teacher"){
                var teacher = await _context.Teachers
                            .Where(x => x.Username == userName)
                            .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                            .ToListAsync();
                ViewData["Teachers"] = new SelectList(teacher, "Username", "FullName");
                //HttpContext.Session.SetString("Name", teacher);
            }
            else{
                var teachers = await _context.Teachers
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();
                ViewData["Teachers"] = new SelectList(teachers, "Username", "FullName");
            }

            // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            
            // Get review types for the select dropdown
            var reviewTypes = _context.ReviewTypes.ToList();
            ViewData["ReviewTypes"] = new SelectList(reviewTypes, "ID", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Content,Type,Student_Username,Teacher_Username,Date")] Review review)
        {
            if (ModelState.IsValid)
            {
                // Patikriname, ar egzistuoja studentas ir mokytojas
                var studentExists = await _context.Students.AnyAsync(s => s.Username == review.Student_Username);
                var teacherExists = await _context.Teachers.AnyAsync(t => t.Username == review.Teacher_Username);
                
                if (!studentExists || !teacherExists)
                {
                    if (!studentExists)
                        TempData["ErrorMessage"] = "Mokinys su šiuo vartotojo vardu neegzistuoja.";
                    if (!teacherExists)
                        TempData["ErrorMessage"] = "Mokytojas su šiuo vartotojo vardu neegzistuoja.";
                    
                    
                    var reviewTypes = _context.ReviewTypes.ToList();
                    ViewData["ReviewTypes"] = new SelectList(reviewTypes, "ID", "Name");
                    return View(review);
                }

                _context.Add(review);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Atsiliepimas sėkmingai pridėtas.";

                return RedirectToAction(nameof(Index)); // Po sėkmingo išsaugojimo nukreipiame į peržiūros puslapį
            }

            TempData["ErrorMessage"] = "Negalima pridėti atsiliepimo.";
            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            var teachers = await _context.Teachers
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

                    // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            ViewData["Teachers"] = new SelectList(teachers, "Username", "FullName");
            ViewData["ReviewTypes"] = await GetReviewTypesSelectListAsync(review.Type);
            return View(review);
        }

        // Atskiras metodas, kuris grąžina SelectList su tipais
        public async Task<SelectList> GetReviewTypesSelectListAsync(int selectedTypeId)
        {
            // Užkrauname visus tipus iš duomenų bazės
            var reviewTypes = await _context.ReviewTypes.ToListAsync();
            
            // Grąžiname SelectList su ID ir Name, kad būtų rodomi pavadinimai ir pasirinktų tipų ID
            return new SelectList(reviewTypes, "ID", "Name", selectedTypeId);
        }

        public async Task<IActionResult> Review(int id)
        {
            var review = await _context.Reviews
                                    .Where(r => r.ID == id)
                                    .Select(r => new ReviewM
                                    {
                                        ID = r.ID,
                                        Date = r.Date,
                                        Content = r.Content,
                                        // Pakeičiame tipą į string, užkraunant tipo pavadinimą
                                        Type = _context.ReviewTypes
                                                        .Where(t => t.ID == r.Type)
                                                        .Select(t => t.Name)
                                                        .FirstOrDefault(),
                                        Student_Name = _context.Students
                                                                        .Where(m => m.Username == r.Student_Username) // Palyginame su Student_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname studento vardą
                                                                        .FirstOrDefault(),
                                                    // Užkrauname mokytojo vardą pagal Teacher_Username
                                                    Teacher_Name = _context.Teachers
                                                                        .Where(m => m.Username == r.Teacher_Username) // Palyginame su Teacher_Username
                                                                        .Select(m => m.Name + " " + m.Surname) // Grąžiname mokytojo vardą
                                                                        .FirstOrDefault()
                                    })
                                    .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }
        public async Task<IActionResult> Edit(int id)
        {
            // Fetching the review asynchronously
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ID == id);
            if (review == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            var teacher = await _context.Teachers
                .Where(t => t.Username == review.Teacher_Username)
                .Select(t => t.Name + " " + t.Surname)
                .FirstOrDefaultAsync();

                    // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            ViewData["TeacherName"] = teacher;

            ViewData["ReviewTypes"] = await GetReviewTypesSelectListAsync(review.Type);

            // Return the view with the review data
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Content,Type,Student_Username,Teacher_Username")] Review review)
        {
            if (id != review.ID)
            {
                return NotFound();
            }

            // Patikriname, ar mokytojas egzistuoja
            var teacherExists = await _context.Teachers.AnyAsync(t => t.Username == review.Teacher_Username);
            if (!teacherExists)
            {
                TempData["ErrorMessage"] = "Mokytojas su šiuo vartotojo vardu neegzistuoja.";
                // Jei yra klaida, grąžiname į redagavimo puslapį su atnaujinta peržiūros tipų lista
                var st = await _context.Students
                    .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                    .ToListAsync();

                        // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
                ViewData["Students"] = new SelectList(st, "Username", "FullName");
                ViewData["ReviewTypes"] = await GetReviewTypesSelectListAsync(review.Type);
                return View(review);
            }

            // Patikriname, ar ModelState yra galiojantis
            if (ModelState.IsValid)
            {
                try
                {
                    var existingReview = await _context.Reviews.FirstOrDefaultAsync(r => r.ID == id);
                    if (existingReview == null)
                    {
                        return NotFound();
                    }

                    existingReview.Date = review.Date;
                    existingReview.Content = review.Content;
                    existingReview.Type = review.Type;
                    existingReview.Student_Username = review.Student_Username;
                    existingReview.Teacher_Username = review.Teacher_Username;

                    _context.Update(existingReview);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Atsiliepimas sėkmingai koreguotas.";

                    return RedirectToAction(nameof(Index)); // Sėkmingai atnaujinus, nukreipiame į pagrindinį puslapį
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            
            // Jei ModelState nėra galiojantis, grąžiname į puslapį su klaidomis
            TempData["ErrorMessage"] = "Klaida redaguojant atsiliepimą.";
            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();
                    // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            ViewData["ReviewTypes"] = await GetReviewTypesSelectListAsync(review.Type);
            return View(review);
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                TempData["ErrorMessage"] = "Atsiliepimas nerastas.";
                return RedirectToAction(nameof(Index));
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Atsiliepimas sėkmingai ištrintas.";
            return RedirectToAction(nameof(Index));
        }
    }
}
