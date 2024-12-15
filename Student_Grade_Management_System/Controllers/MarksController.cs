using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Student_Grade_Management_System.Controllers
{
    public class MarksController : Controller
    {
        private readonly SystemDbContext _context;

        public MarksController(SystemDbContext context)
        {
            _context = context;
        }
        // Centralized properties for Role and Username
        private string UserRole
        {
            get
            {
                return User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "Teacher";
            }
        }

        private string Username
        {
            get
            {
                return User?.Identity?.Name ?? "T-Jūratė";
            }
        }
        public async Task<IActionResult> VisualizeGrades()
        {
            ViewBag.Role = UserRole;

            var query = _context.Grades
                .Join(
                    _context.Lessons,
                    grade => grade.Subject_ID,
                    lesson => lesson.ID,
                    (grade, lesson) => new { Grade = grade, Lesson = lesson })
                .Join(
                    _context.Subjects,
                    lessonCombined => lessonCombined.Lesson.Subject_ID,
                    subject => subject.ID,
                    (lessonCombined, subject) => new { lessonCombined.Grade, SubjectName = subject.Name, lessonCombined.Lesson.Teacher_Username })
                .Join(
                    _context.Students,
                    gradeCombined => gradeCombined.Grade.Student_Username,
                    student => student.Username,
                    (gradeCombined, student) => new { gradeCombined.Grade, gradeCombined.SubjectName, gradeCombined.Teacher_Username, StudentName = student.Name });

            switch (UserRole)
            {
                case "Student":
                    query = query.Where(g => g.Grade.Student_Username == Username);

                    var studentGrades = await query
                        .Select(g => new
                        {
                            g.StudentName,
                            g.SubjectName,
                            g.Grade.Value,
                            g.Grade.Date
                        })
                        .OrderBy(g => g.Date)
                        .ToListAsync();

                    return View("GradeGraph", studentGrades);

                case "Teacher":
                    query = query.Where(g => g.Teacher_Username == Username);

                    var teacherGrades = await query
                        .Select(g => new
                        {
                            g.StudentName,
                            g.SubjectName,
                            g.Grade.Value,
                            g.Grade.Date
                        })
                        .OrderBy(g => g.StudentName)
                        .ThenBy(g => g.SubjectName)
                        .ThenBy(g => g.Date)
                        .ToListAsync();

                    return View("GradeGraph", teacherGrades);

                case "Parent":
                    var childrenUsernames = await _context.ParentsOfStudents
                        .Where(p => p.Parent_Username == Username)
                        .Select(p => p.Student_Username)
                        .ToListAsync();

                    query = query.Where(g => childrenUsernames.Contains(g.Grade.Student_Username));

                    var parentGrades = await query
                        .Select(g => new
                        {
                            g.StudentName,
                            g.SubjectName,
                            g.Grade.Value,
                            g.Grade.Date
                        })
                        .OrderBy(g => g.StudentName)
                        .ThenBy(g => g.SubjectName)
                        .ThenBy(g => g.Date)
                        .ToListAsync();

                    return View("GradeGraph", parentGrades);

                case "Admin":
                    var adminGrades = await query
                        .Select(g => new
                        {
                            g.StudentName,
                            g.SubjectName,
                            g.Grade.Value,
                            g.Grade.Date
                        })
                        .OrderBy(g => g.StudentName)
                        .ThenBy(g => g.SubjectName)
                        .ThenBy(g => g.Date)
                        .ToListAsync();

                    return View("GradeGraph", adminGrades);

                default:
                    return Unauthorized();
            }
        }


























        //string role = "Teacher", string username = "T-Jūratė"
        //string role = "Student", string username = "S-Agne"
        public async Task<IActionResult> CalculateAverage()
        {
            ViewBag.Role = UserRole;

            // Join grades with lessons, subjects, students, and weights
            var query = _context.Grades
                .Join(
                    _context.Lessons,
                    grade => grade.Subject_ID,
                    lesson => lesson.ID,
                    (grade, lesson) => new { Grade = grade, Lesson = lesson })
                .Join(
                    _context.Subjects,
                    lessonCombined => lessonCombined.Lesson.Subject_ID,
                    subject => subject.ID,
                    (lessonCombined, subject) => new { lessonCombined.Grade, SubjectName = subject.Name, lessonCombined.Lesson.Teacher_Username })
                .Join(
                    _context.Students,
                    gradeCombined => gradeCombined.Grade.Student_Username,
                    student => student.Username,
                    (gradeCombined, student) => new { gradeCombined.Grade, gradeCombined.SubjectName, gradeCombined.Teacher_Username, StudentName = student.Name })
                .Join(
                    _context.GradeWeights,
                    gradeCombined => gradeCombined.Grade.Weight_ID,
                    weight => weight.ID,
                    (gradeCombined, weight) => new { gradeCombined.Grade, gradeCombined.SubjectName, gradeCombined.Teacher_Username, gradeCombined.StudentName, Weight = weight.Weight });

            switch (UserRole)
            {
                case "Student":
                    query = query.Where(g => g.Grade.Student_Username == Username);

                    var studentData = await query
                        .GroupBy(g => g.SubjectName)
                        .Select(group => new
                        {
                            SubjectName = group.Key,
                            Average = group.Sum(g => g.Grade.Value * g.Weight) / group.Sum(g => g.Weight)
                        })
                        .ToListAsync();

                    var overallStudentAverage = studentData.Any()
                        ? studentData.Average(g => g.Average)
                        : 0;

                    ViewBag.OverallAverage = overallStudentAverage;
                    return View("Averages", studentData);

                case "Teacher":
                    query = query.Where(g => g.Teacher_Username == Username);

                    var teacherData = await query
                        .GroupBy(g => new { g.StudentName, g.SubjectName })
                        .Select(group => new
                        {
                            StudentName = group.Key.StudentName,
                            SubjectName = group.Key.SubjectName,
                            Average = group.Sum(g => g.Grade.Value * g.Weight) / group.Sum(g => g.Weight)
                        })
                        .OrderBy(data => data.StudentName)
                        .ThenBy(data => data.SubjectName)
                        .ToListAsync();

                    var overallTeacherAverage = teacherData.Any()
                        ? teacherData.Average(g => g.Average)
                        : 0;

                    ViewBag.OverallAverage = overallTeacherAverage;
                    return View("Averages", teacherData);

                case "Parent":
                    var childrenUsernames = await _context.ParentsOfStudents
                        .Where(p => p.Parent_Username == Username)
                        .Select(p => p.Student_Username)
                        .ToListAsync();

                    query = query.Where(g => childrenUsernames.Contains(g.Grade.Student_Username));

                    var parentData = await query
                        .GroupBy(g => new { g.StudentName, g.SubjectName })
                        .Select(group => new
                        {
                            StudentName = group.Key.StudentName,
                            SubjectName = group.Key.SubjectName,
                            Average = group.Sum(g => g.Grade.Value * g.Weight) / group.Sum(g => g.Weight)
                        })
                        .OrderBy(data => data.StudentName)
                        .ThenBy(data => data.SubjectName)
                        .ToListAsync();

                    var overallParentAverage = parentData.Any()
                        ? parentData.Average(g => g.Average)
                        : 0;

                    ViewBag.OverallAverage = overallParentAverage;
                    return View("Averages", parentData);

                case "Admin":
                    var adminData = await query
                        .GroupBy(g => new { g.StudentName, g.SubjectName })
                        .Select(group => new
                        {
                            StudentName = group.Key.StudentName,
                            SubjectName = group.Key.SubjectName,
                            Average = group.Sum(g => g.Grade.Value * g.Weight) / group.Sum(g => g.Weight)
                        })
                        .OrderBy(data => data.StudentName)
                        .ThenBy(data => data.SubjectName)
                        .ToListAsync();

                    var overallAdminAverage = adminData.Any()
                        ? adminData.Average(g => g.Average)
                        : 0;

                    ViewBag.OverallAverage = overallAdminAverage;
                    return View("Averages", adminData);

                default:
                    return Unauthorized();
            }
        }














        public IActionResult Edit(int id)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.ID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade); // Tik jei naudojamas atskiras redagavimo puslapis.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int value, DateTime date)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            try
            {
                grade.Value = value;
                grade.Date = date;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating grade: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var grade = _context.Grades.FirstOrDefault(g => g.ID == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade); // Praleidžia duomenis į trynimo peržiūrą, jei reikia.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            try
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync(); // Ensure the change is committed to the database
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting grade: {ex.Message}");
                // Optionally, you can add user feedback here (e.g., ViewBag.ErrorMessage = "Could not delete the grade.")
            }

            return RedirectToAction(nameof(Index)); // Reload the index after deletion
        }




        public IActionResult Create()
        {
            // Kietai užkoduota rolė ir mokytojo username
            var role = UserRole;
            var teacherUsername = Username;

            // Check role
            if (role != "Teacher")
            {
                return Unauthorized();
            }

            // Užkrauname mokinius
            ViewBag.Students = _context.Students.Select(s => new { s.Username, FullName = s.Name + " " + s.Surname }).ToList();

            // Užkrauname dalykus
            ViewBag.Subjects = _context.Subjects.Select(s => new { s.ID, s.Name }).ToList();

            // Užkrauname pažymių tipus
            ViewBag.GradeTypes = _context.GradeTypes.Select(t => new { t.ID, t.Name }).ToList();

            // Filtruojame pamokas pagal mokytoją
            ViewBag.Lessons = _context.Lessons
                .Where(l => l.Teacher_Username == teacherUsername)
                .Select(l => new
                {
                    l.ID,
                    LessonInfo = $"{l.Day}, {l.StartTime} - {l.EndTime} ({_context.Subjects.FirstOrDefault(s => s.ID == l.Subject_ID).Name})"
                })
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string gradeTypeId, string lessonId, Grade newGrade)
        {
            // Kietai užkoduota rolė ir mokytojo username POST metu
            var role = UserRole;
            var teacherUsername = Username;

            if (role != "Teacher")
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Randame svorį pagal pasirinktą tipą
                    var selectedType = await _context.GradeTypes
                        .Where(t => t.ID.ToString() == gradeTypeId)
                        .FirstOrDefaultAsync();

                    if (selectedType != null)
                    {
                        var weight = await _context.GradeWeights
                            .Where(w => w.Type == selectedType.ID)
                            .FirstOrDefaultAsync();

                        if (weight != null)
                        {
                            newGrade.Weight_ID = weight.ID; // Priskiriame svorio ID
                        }
                        else
                        {
                            ModelState.AddModelError("", "Pasirinktam tipui nerastas svoris.");
                            ReloadDropdowns(teacherUsername);
                            return View(newGrade);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Pasirinktas tipas neegzistuoja.");
                        ReloadDropdowns(teacherUsername);
                        return View(newGrade);
                    }

                    // Priskiriame pamokos ID
                    newGrade.Subject_ID = int.Parse(lessonId);

                    // Įrašome pažymį į duomenų bazę
                    _context.Grades.Add(newGrade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Klaida įrašant duomenis: {ex.Message}");
                }
            }

            // Jei yra klaidų, perkrauname duomenis
            ReloadDropdowns(teacherUsername);
            return View(newGrade);
        }

        // Metodas dropdown'ams perkrauti
        private void ReloadDropdowns(string teacherUsername)
        {
            ViewBag.Students = _context.Students.Select(s => new { s.Username, FullName = s.Name + " " + s.Surname }).ToList();
            ViewBag.Subjects = _context.Subjects.Select(s => new { s.ID, s.Name }).ToList();
            ViewBag.GradeTypes = _context.GradeTypes.Select(t => new { t.ID, t.Name }).ToList();
            ViewBag.Lessons = _context.Lessons
                .Where(l => l.Teacher_Username == teacherUsername)
                .Select(l => new
                {
                    l.ID,
                    LessonInfo = $"{l.Day}, {l.StartTime} - {l.EndTime} ({_context.Subjects.FirstOrDefault(s => s.ID == l.Subject_ID).Name})"
                })
                .ToList();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Role = UserRole;

            // Užklausos šablonas
            var query = _context.Grades
                .Join(
                    _context.GradeWeights,
                    grade => grade.Weight_ID,
                    weight => weight.ID,
                    (grade, weight) => new
                    {
                        Grade = grade,
                        Weight = weight.Weight,
                        TypeID = weight.Type
                    })
                .Join(
                    _context.GradeTypes,
                    combined => combined.TypeID,
                    gradeType => gradeType.ID,
                    (combined, gradeType) => new
                    {
                        combined.Grade,
                        combined.Weight,
                        GradeTypeName = gradeType.Name
                    })
                .Join(
                    _context.Lessons,
                    combined => combined.Grade.Subject_ID,
                    lesson => lesson.ID,
                    (combined, lesson) => new
                    {
                        combined.Grade,
                        combined.Weight,
                        combined.GradeTypeName,
                        Lesson = lesson
                    })
                .Join(
                    _context.Subjects,
                    lessonCombined => lessonCombined.Lesson.Subject_ID,
                    subject => subject.ID,
                    (lessonCombined, subject) => new
                    {
                        lessonCombined.Grade,
                        lessonCombined.Weight,
                        lessonCombined.GradeTypeName,
                        lessonCombined.Lesson,
                        SubjectName = subject.Name
                    })
                .Join(
                    _context.Students,
                    gradeCombined => gradeCombined.Grade.Student_Username,
                    student => student.Username,
                    (gradeCombined, student) => new
                    {
                        gradeCombined.Grade,
                        gradeCombined.Weight,
                        gradeCombined.GradeTypeName,
                        gradeCombined.Lesson,
                        gradeCombined.SubjectName,
                        StudentName = student.Name,
                        StudentSurname = student.Surname, // Pridėta pavardė
                        Class = student.Class_Number + " " + student.Class_Letter
                    });

            // Filtravimas pagal naudotojo rolę
            switch (UserRole)
            {
                case "Student":
                    query = query.Where(g => g.Grade.Student_Username == Username);
                    break;

                case "Teacher":
                    query = query.Where(g => g.Lesson.Teacher_Username == Username);
                    break;

                case "Parent":
                    var childrenUsernames = await _context.ParentsOfStudents
                        .Where(p => p.Parent_Username == Username)
                        .Select(p => p.Student_Username)
                        .ToListAsync();

                    query = query.Where(g => childrenUsernames.Contains(g.Grade.Student_Username));
                    break;

                case "Admin":
                    // Admin mato viską
                    break;

                default:
                    return Unauthorized();
            }

            // Užkrauname pažymius
            var grades = await query
                .Select(g => new
                {
                    g.Grade.ID,
                    g.Grade.Value,
                    g.Grade.Date,
                    StudentFullName = g.StudentName + " " + g.StudentSurname, // Sukuriamas pilnas vardas ir pavardė
                    g.SubjectName,
                    GradeTypeName = g.GradeTypeName,
                    g.Class,
                    LessonID = g.Lesson.ID
                })
                .OrderBy(g => g.StudentFullName) // Rikiuojama pagal pilną vardą
                .ThenBy(g => g.SubjectName)
                .ThenBy(g => g.Date)
                .ToListAsync();

            return View(grades);
        }



        public async Task<IActionResult> FilteredIndex(string classFilter, string subjectFilter)
        {
            ViewBag.Role = UserRole;

            // Užklausos šablonas
            var query = _context.Grades
                .Join(
                    _context.GradeWeights,
                    grade => grade.Weight_ID,
                    weight => weight.ID,
                    (grade, weight) => new
                    {
                        grade.ID,
                        grade.Value,
                        grade.Date,
                        grade.Student_Username,
                        grade.Subject_ID,
                        Weight = weight.Weight,
                        TypeID = weight.Type
                    })
                .Join(
                    _context.GradeTypes,
                    combined => combined.TypeID,
                    gradeType => gradeType.ID,
                    (combined, gradeType) => new
                    {
                        combined.ID,
                        combined.Value,
                        combined.Date,
                        combined.Student_Username,
                        combined.Subject_ID,
                        combined.Weight,
                        GradeTypeName = gradeType.Name
                    })
                .Join(
                    _context.Lessons,
                    combined => combined.Subject_ID,
                    lesson => lesson.ID,
                    (combined, lesson) => new
                    {
                        combined.ID,
                        combined.Value,
                        combined.Date,
                        combined.Student_Username,
                        combined.Weight,
                        combined.GradeTypeName,
                        Subject_ID = lesson.Subject_ID
                    })
                .Join(
                    _context.Subjects,
                    lessonCombined => lessonCombined.Subject_ID,
                    subject => subject.ID,
                    (lessonCombined, subject) => new
                    {
                        lessonCombined.ID,
                        lessonCombined.Value,
                        lessonCombined.Date,
                        lessonCombined.Student_Username,
                        lessonCombined.Weight,
                        lessonCombined.GradeTypeName,
                        SubjectName = subject.Name
                    })
                .Join(
                    _context.Students,
                    gradeCombined => gradeCombined.Student_Username,
                    student => student.Username,
                    (gradeCombined, student) => new
                    {
                        gradeCombined.ID,
                        gradeCombined.Value,
                        gradeCombined.Date,
                        gradeCombined.Student_Username,
                        gradeCombined.Weight,
                        gradeCombined.GradeTypeName,
                        gradeCombined.SubjectName,
                        Class = UserRole == "Student" ? null : student.Class_Number + " " + student.Class_Letter, // Paslėpti klasę studentams
                        StudentName = student.Name,
                        StudentSurname = student.Surname // Pridėta pavardė
                    });

            // Filtravimas pagal naudotojo rolę
            switch (UserRole)
            {
                case "Student":
                    query = query.Where(g => g.Student_Username == Username);
                    break;

                case "Teacher":
                    // Mokytojai mato viską
                    break;

                case "Parent":
                    var childrenUsernames = await _context.ParentsOfStudents
                        .Where(p => p.Parent_Username == Username)
                        .Select(p => p.Student_Username)
                        .ToListAsync();

                    query = query.Where(g => childrenUsernames.Contains(g.Student_Username));
                    break;

                case "Admin":
                    // Admin mato viską
                    break;

                default:
                    return Unauthorized();
            }

            // Filtravimo logika
            if (UserRole != "Student")
            {
                query = query.Where(g =>
                    (string.IsNullOrEmpty(classFilter) || g.Class == classFilter) &&
                    (string.IsNullOrEmpty(subjectFilter) || g.SubjectName == subjectFilter));
            }
            else
            {
                query = query.Where(g => string.IsNullOrEmpty(subjectFilter) || g.SubjectName == subjectFilter);
            }

            // Sukuriamas pilnas vardas, rikiuojama pagal jį
            var grades = await query
                .Select(g => new
                {
                    g.ID,
                    g.Value,
                    g.Date,
                    StudentFullName = g.StudentName + " " + g.StudentSurname, // Sukuriamas pilnas vardas ir pavardė
                    g.SubjectName,
                    g.GradeTypeName,
                    g.Class
                })
                .OrderBy(g => g.StudentFullName) // Rūšiavimas pagal pilną vardą
                .ThenBy(g => g.SubjectName)
                .ThenBy(g => g.Date)
                .ToListAsync();

            return View("Index", grades);
        }





    }
}
