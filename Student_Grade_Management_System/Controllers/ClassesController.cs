using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;

namespace Student_Grade_Management_System.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SystemDbContext _context;
        public ClassesController(SystemDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.ToListAsync();
            if (classes == null)
            {
                return NotFound();
            }
            return View(classes);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var freeTeachers = await _context.Teachers
            .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
            .ToListAsync();

            ViewBag.FreeTeachers = freeTeachers;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int classNumber, int studentCount, string Teacher_Username)
        {
            var newClass = new Class
            {
                Number = classNumber,
                StudentCount = studentCount,
                Teacher_Username = Teacher_Username
            };

            var classes = await _context.Classes.ToListAsync();
            foreach (var c in classes)
            {
                if (c.Number == classNumber)
                {
                    char lastLetter = c.Letter.Last();
                    newClass.Letter = ((char)(lastLetter + 1)).ToString();
                }

                else
                {
                    newClass.Letter = "a";
                }
            }

            // if there is a class with the same teacher

            await _context.Classes.AddAsync(newClass);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AutoAssign()
        {
            var freeClasses = await _context.Classes
                .Where(c => c.Teacher_Username == null)
                .ToListAsync();

            var freeTeachers = await _context.Teachers
                .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
                .ToListAsync();

            ViewBag.FreeClasses = freeClasses;
            ViewBag.FreeTeachers = freeTeachers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AutoAssignConfirm()
        {
            var freeClasses = await _context.Classes
                .Where(c => c.Teacher_Username == null)
                .ToListAsync();

            var freeTeachers = await _context.Teachers
                .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
                .ToListAsync();

            int teacherIndex = 0;
            foreach (var freeClass in freeClasses)
            {
                if (teacherIndex >= freeTeachers.Count)
                {
                    break;
                }

                freeClass.Teacher_Username = freeTeachers[teacherIndex].Username;
                teacherIndex++;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ManualAssign()
        {
            var classes = await _context.Classes.ToListAsync();
            var freeTeachers = await _context.Teachers
                .Where(t => !_context.Classes.Any(c => c.Teacher_Username == t.Username))
                .ToListAsync();

            ViewBag.FreeTeachers = freeTeachers;

            return View(classes);
        }

        [HttpPost]
        public async Task<IActionResult> AssignTeacher(int classNumber, string classLetter, string teacherUsername)
        {
            var classToUpdate = await _context.Classes.FindAsync(classNumber, classLetter);
            if (classToUpdate != null)
            {
                classToUpdate.Teacher_Username = teacherUsername;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManualAssign));
        }

        [HttpPost]
        public async Task<IActionResult> UnassignTeacher(int classNumber, string classLetter)
        {
            var classToUpdate = await _context.Classes.FindAsync(classNumber, classLetter);
            if (classToUpdate != null)
            {
                classToUpdate.Teacher_Username = null;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManualAssign));
        }

        public IActionResult Create()
        {
            ViewBag.Classes = _context.Classes
                .Select(s => new
                {
                    CombinedOne = s.Number + " " + s.Letter,
                    s.Number,
                    s.Letter
                })
                .Distinct()
                .ToList();

            ViewBag.TeacherSubjects = _context.Subjects
            .Join(
                _context.SubjectsOfTeachers,
                cls => cls.ID,
                timetable => timetable.Subject_ID,
                (cls, timetable) => new { Subject = cls, SubjectAndTeacher = timetable }
            )
            .Join(
                _context.Teachers,
                combined => combined.SubjectAndTeacher.Teacher_Username,
                lesson => lesson.Username,
                (combined, lesson) => new { combined.Subject, combined.SubjectAndTeacher, Teacher = lesson }
            )
            .GroupBy(data => data.Subject.ID)
            .Select(group => new
            {
                SubjectName = group.FirstOrDefault().Subject.Name,  // Assuming you want the name of the subject
                SubjectID = group.Key,  // Using the grouped ID
                Teachers = group.Select(g => new {Username=g.Teacher.Username, Name=g.Teacher.Name, Surname =g.Teacher.Surname }).ToList() // List of teacher names for the subject
            })
            .ToList();


            return View();
        }
    }
}
