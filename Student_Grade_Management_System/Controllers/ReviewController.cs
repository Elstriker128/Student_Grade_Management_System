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
            var reviews = await _context.Reviews
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
                                                    Student_Username = r.Student_Username,
                                                    Teacher_Username = r.Teacher_Username
                                                })
                                                .ToListAsync();
            if (reviews == null)
            {
                return NotFound();
            }
            return View(reviews);
        }

        public IActionResult Add()
        {
            return View();
        }

        // Atskiras metodas, kuris grąžina SelectList su tipais
        public async Task<SelectList> GetReviewTypesSelectListAsync(int selectedTypeId)
        {
            // Užkrauname visus tipus iš duomenų bazės
            var reviewTypes = await _context.ReviewTypes.ToListAsync();
            
            // Grąžiname SelectList su ID ir Name, kad būtų rodomi pavadinimai ir pasirinktų tipų ID
            return new SelectList(reviewTypes, "ID", "Name", selectedTypeId);
        }

        public async Task<IActionResult> Edit(int id)
        {
            // Fetching the review asynchronously
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.ID == id);
            if (review == null)
            {
                return NotFound();
            }

            ViewData["ReviewTypes"] = await GetReviewTypesSelectListAsync(review.Type);

            // Return the view with the review data
            return View(review);
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
                                        Student_Username = r.Student_Username,
                                        Teacher_Username = r.Teacher_Username
                                    })
                                    .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound();
            }

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

            // Patikriname, ar mokytojo_useris egzistuoja
            var teacherExists = await _context.Teachers.AnyAsync(t => t.Username == review.Teacher_Username);
            if (!teacherExists)
            {
                ModelState.AddModelError("Teacher_Username", "Mokytojas su šiuo vartotojo vardu neegzistuoja.");
                return View(review);  // Grąžiname su klaida
            }

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
        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
    }
}
