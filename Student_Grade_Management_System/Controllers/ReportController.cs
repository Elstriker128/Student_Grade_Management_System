using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Student_Grade_Management_System.Controllers
{
    public class ReportController : Controller
    {
        private readonly SystemDbContext _context;

        public ReportController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            var subjects = _context.Subjects.ToList();
            ViewData["Subjects"] = new SelectList(subjects, "ID", "Name");
            return View();
        }

        // [HttpPost]
        // public async Task<IActionResult> GenerateReport([FromBody] ReportFilters filters)
        // {
        //     var query = _context.Grades
        //         .Include(i => i.Student_Username)
        //         .Include(i => i.Subject_ID)
        //         .Include(i => i.IvertinimoSvoris)
        //         .AsQueryable();

        //     // Filtruoti pagal mokinį
        //     if (!string.IsNullOrEmpty(filters.Student))
        //     {
        //         query = query.Where(i => i.Mokinis.MokinioUseris == filters.Student);
        //     }

        //     // Filtruoti pagal datas
        //     if (!string.IsNullOrEmpty(filters.StartDate))
        //     {
        //         var startDate = DateTime.Parse(filters.StartDate);
        //         query = query.Where(i => i.Data >= startDate);
        //     }

        //     if (!string.IsNullOrEmpty(filters.EndDate))
        //     {
        //         var endDate = DateTime.Parse(filters.EndDate);
        //         query = query.Where(i => i.Data <= endDate);
        //     }

        //     // Filtruoti pagal dalyką
        //     if (!string.IsNullOrEmpty(filters.Subject))
        //     {
        //         query = query.Where(i => i.Pamoka.Dalykas.Pavadinimas == filters.Subject);
        //     }

        //     // Filtruoti pagal įvertinimo intervalą
        //     if (int.TryParse(filters.GradeMin, out int gradeMin) && int.TryParse(filters.GradeMax, out int gradeMax))
        //     {
        //         query = query.Where(i => i.Pazymys >= gradeMin && i.Pazymys <= gradeMax);
        //     }

        //     var reportData = await query
        //         .Select(i => new
        //         {
        //             student = i.Mokinis.Vardas + " " + i.Mokinis.Pavarde,
        //             subject = i.Pamoka.Dalykas.Pavadinimas,
        //             grade = i.Pazymys,
        //             date = i.Data.ToString("yyyy-MM-dd")
        //         })
        //         .ToListAsync();

        //     return Json(reportData);
        // }

        public class ReportFilters
        {
            public string Student { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Subject { get; set; }
            public string GradeMin { get; set; }
            public string GradeMax { get; set; }
        }

    }
}