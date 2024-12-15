using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Student_Grade_Management_System.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly SystemDbContext _context;
        public ScheduleController(SystemDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            ViewBag.Times = _context.Lessons.Select(s => new
            {
                CombinedOne = s.StartTime + "-" + s.EndTime,
                s.StartTime,
                s.EndTime
            }).Distinct().OrderBy(s => s.StartTime).ToList();

            // Get username from session
            var username = HttpContext.Session.GetString("Username");

            var neededNumber = 0;
            var neededLetter = string.Empty;

            if (username != null)
            {
                // Attempt to find the student by username
                var userInfo = _context.Students.FirstOrDefault(s => s.Username == username);
                if (userInfo != null)
                {
                    HttpContext.Session.SetString("ClassLetterNumber", $"{userInfo.Class_Number} {userInfo.Class_Letter}");

                    var classInfo = HttpContext.Session.GetString("ClassLetterNumber");
                    if (!string.IsNullOrEmpty(classInfo))
                    {
                        var classParts = classInfo.Split(" "); // Adjust this logic to split the value as required
                        neededNumber = int.Parse(classParts[0]);
                        neededLetter = classParts[1];
                    }
                }
            }

            ViewBag.StudentSchedule = _context.Classes
            .Join(
                _context.Timetables, // Join Classes with Timetables
                cls => new { cls.Number, cls.Letter }, // Composite key from Classes
                timetable => new { Number = timetable.Class_Number, Letter = timetable.Class_Letter }, // Null check for nullable int in Timetables
                (cls, timetable) => new { Class = cls, Timetable = timetable } // Result is an anonymous object
            )
            .Join(
                _context.Lessons, // Join the result with Lessons
                combined => combined.Timetable.Lesson_ID, // Key from Timetables
                lesson => lesson.ID, // Key from Lessons
                (combined, lesson) => new { combined.Class, combined.Timetable, Lesson = lesson } // Result is an anonymous object
            )
            .Join(
                _context.Subjects, // Join with the Subjects table
                combined => combined.Lesson.Subject_ID, // Key from Lessons (assuming the Lessons table has a SubjectId field)
                subject => subject.ID, // Key from Subjects table
                (combined, subject) => new { combined.Class, combined.Timetable, combined.Lesson, Subject = subject } // Result is an anonymous object with Subject
            )
            .Where(data => data.Class.Number == neededNumber && data.Class.Letter == neededLetter)
            .GroupBy(data => new { data.Lesson.StartTime, data.Lesson.EndTime }) // Group by class details
            .Select(group => new
            {
                StartTime = group.Key.StartTime,
                EndTime = group.Key.EndTime,
                Lessons = group.Select(x => new
                {
                    x.Lesson.Day,
                    x.Subject.Name,// Replace with the actual field you want from Lessons
                }).ToList()
            })
            .ToList();


            return View();
        }
    }
}
