using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Grade_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.Json;

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
            var userType = HttpContext.Session.GetString("UserType");
            var userName = HttpContext.Session.GetString("Username");

            var students = await _context.Students
                .Select(s => new { s.Username, FullName = s.Name + " " + s.Surname })
                .ToListAsync();

            // Sukuriame SelectList su Username ir FullName, kad būtų galima pasirinkti vardą ir pavardę
            ViewData["Students"] = new SelectList(students, "Username", "FullName");
            var subjects = _context.Subjects.ToList();
            ViewData["Subjects"] = new SelectList(subjects, "ID", "Name");

            TempData["UserType"] = userType;
            TempData["Username"] = userName;
            return View();
        }

        [HttpPost]
        [Route("Report/Index")]
        public async Task<IActionResult> Index([FromBody] ReportFilters filters)
        {
            var queryGrade = _context.Grades.AsQueryable();
            var queryReview = _context.Reviews.AsQueryable();
            Console.WriteLine(filters.Subject);

            // Filtruoti pagal mokinį
            if (!string.IsNullOrEmpty(filters.Student))
            {
                queryGrade = queryGrade.Where(i => i.Student_Username == filters.Student);
                queryReview = queryReview.Where(i => i.Student_Username == filters.Student);
            }

            // Filtruoti pagal datas
            if (!string.IsNullOrEmpty(filters.StartDate))
            {
                var startDate = DateTime.Parse(filters.StartDate);
                queryGrade = queryGrade.Where(i => i.Date >= startDate);
                queryReview = queryReview.Where(i => i.Date >= startDate);
            }

            if (!string.IsNullOrEmpty(filters.EndDate))
            {
                var endDate = DateTime.Parse(filters.EndDate);
                queryGrade = queryGrade.Where(i => i.Date <= endDate);
                queryReview = queryReview.Where(i => i.Date <= endDate);
            }

            // Filtruoti pagal dalyką
            if (!string.IsNullOrEmpty(filters.Subject))
            {
                var subjectId = _context.Subjects
                            .Where(i => i.Name == filters.Subject)
                            .Select(i => i.ID)
                            .FirstOrDefault();

                var lessonId = await  _context.Lessons
                                .Where(i => i.Subject_ID == subjectId)
                                .Select(i => i.ID)
                                .ToListAsync();

                queryGrade = queryGrade.Where(i => lessonId.Contains(i.Subject_ID));
            }

            // Filtruoti pagal įvertinimo intervalą
            if (int.TryParse(filters.GradeMin, out int gradeMin) && int.TryParse(filters.GradeMax, out int gradeMax))
            {
                queryGrade = queryGrade.Where(i => i.Value >= gradeMin && i.Value <= gradeMax);
            }

            var reportData = await queryGrade
                .Select(i => new
                {
                    student = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Name + " " + m.Surname)
                                .FirstOrDefault(),
                    classid = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Class_Number + m.Class_Letter)
                                .FirstOrDefault(),
                    subject = _context.Subjects
                                .Where(k => k.ID == _context.Lessons
                                                    .Where(j => j.ID == i.Subject_ID)
                                                    .Select(m => m.Subject_ID)
                                                    .FirstOrDefault())
                                .Select(k => k.Name)
                                .FirstOrDefault(),
                    grade = i.Value,
                    date = i.Date.ToString("yyyy-MM-dd")
                })
                .ToListAsync(); 

            var reviewData = await queryReview
                .Select(i => new 
                {
                    student = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Name + " " + m.Surname)
                                .FirstOrDefault(),
                    type = _context.ReviewTypes
                                .Where(j => j.ID == i.Type)
                                .Select(m => m.Name)
                                .FirstOrDefault(),
                    review = i.Content,
                    date = i.Date.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            var groupedData = reportData
                .GroupBy(r => r.student)
                .Select(g => new 
                {
                    student = g.Key,
                    records = g.ToList(),
                    reviews = reviewData.Where(r => r.student == g.Key).ToList()
                })
                .ToList();

            var reportData2 = await queryGrade
                .Select(i => new ReportRecord
                {
                    StudentName = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Name + " " + m.Surname)
                                .FirstOrDefault(),
                    Class = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Class_Number + m.Class_Letter)
                                .FirstOrDefault(),
                    Subject = _context.Subjects
                                .Where(k => k.ID == _context.Lessons
                                                    .Where(j => j.ID == i.Subject_ID)
                                                    .Select(m => m.Subject_ID)
                                                    .FirstOrDefault())
                                .Select(k => k.Name)
                                .FirstOrDefault(),
                    Grade = i.Value,
                    Date = i.Date.ToString("yyyy-MM-dd")
                })
                .ToListAsync(); 

            var reviewData2 = await queryReview
                .Select(i => new ReportReview
                {
                    StudentName = _context.Students
                                .Where(j => j.Username == i.Student_Username)
                                .Select(m => m.Name + " " + m.Surname)
                                .FirstOrDefault(),
                    Type = _context.ReviewTypes
                                .Where(j => j.ID == i.Type)
                                .Select(m => m.Name)
                                .FirstOrDefault(),
                    Review = i.Content,
                    Date = i.Date.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            TempData["reportData"] = JsonSerializer.Serialize(reportData2);
            TempData["reviewData"] = JsonSerializer.Serialize(reviewData2);

            return Json(groupedData);
        }

        private async Task<(List<ReportRecord>, List<ReportReview>)> GenerateReportData(string student)
        {
            var reportData = JsonSerializer.Deserialize<List<ReportRecord>>(TempData["reportData"].ToString());
            var reviewData = JsonSerializer.Deserialize<List<ReportReview>>(TempData["reviewData"].ToString());

            // Filter by student
            reportData = reportData.Where(j => j.StudentName == student).ToList();
            reviewData = reviewData.Where(j => j.StudentName == student).ToList();

            return (reportData, reviewData);
        }

        public async Task<IActionResult> Export(string student, string format)
        {
            // Pirmiausia generuoti ataskaitą
            var (reportData, reviewData) = await GenerateReportData(student);
            Console.WriteLine($"reportData count: {reportData.Count}");

            int columnWidth = 20;

            if (format == "csv")
            {
                var csv = GenerateCsv(reportData, reviewData);
                var csvBytes = Encoding.UTF8.GetBytes(csv); // Užtikrinti, kad CSV būtų sugeneruotas su UTF-8 kodavimu
                return File(csvBytes, "text/csv", $"{student}_ataskaita.csv");
            }
            else if (format == "txt")
            {
                var txt = GenerateTxt(reportData, reviewData, columnWidth);
                var txtBytes = Encoding.UTF8.GetBytes(txt);
                return File(txtBytes, "text/plain", $"{student}_ataskaita.txt");
            }
            else if (format == "docx")
            {
                var docxFile = GenerateDocx(reportData, reviewData);
                return File(docxFile, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{student}_ataskaita.docx");
            }

            return BadRequest("Nepalaikomas formatas");
        }

        private string GenerateCsv(List<ReportRecord> data, List<ReportReview> data2)
        {
            var sb = new StringBuilder();
            sb.Append('\uFEFF');
            sb.AppendLine("Studentas;Klasė;Dalykas;Įvertinimas;Data");

            foreach (var record in data)
            {
                sb.AppendLine($"{record.StudentName};{record.Class};{record.Subject};{record.Grade};{record.Date}");
            }
            if (data2.Count > 0){
                sb.AppendLine("");
                sb.AppendLine("Atsiliepimo tipas;Data;Atsiliepimas");
                foreach (var record in data2)
                {
                    sb.AppendLine($"{record.Type};{record.Date};{record.Review}");
                }
            }

            return sb.ToString();
        }

        private string GenerateTxt(List<ReportRecord> data, List<ReportReview> data2, int columnWidth)
        {
            int totalLength = (columnWidth * 5) + 4 * 3;
            var sb = new StringBuilder();
            sb.AppendLine("Mokinio ataskaita");
            sb.AppendLine(new string('-', totalLength));

            // Sukuriame formatą, kad visi stulpeliai būtų tokio paties pločio
            string format = $"{{0,-{columnWidth}}} | {{1,-{columnWidth}}} | {{2,-{columnWidth}}} | {{3,-{columnWidth}}} | {{4,-{columnWidth}}}";

            // Antraštės eilutė
            sb.AppendLine(string.Format(format, "Studentas", "Klasė", "Dalykas", "Įvertinimas", "Data"));

            // Įrašyti duomenis apie mokinius
            foreach (var record in data)
            {
                sb.AppendLine(string.Format(format, record.StudentName, record.Class, record.Subject, record.Grade, record.Date));
            }

            // Jei yra atsiliepimų, įrašyti ir juos
            if (data2.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Atsiliepimai:");
                sb.AppendLine(new string('-', totalLength));
                
                // Formatavimas atsiliepimų stulpeliams
                string reviewFormat = $"{{0,-{columnWidth}}} | {{1,-{columnWidth}}} | {{2,-{columnWidth}}}";
                sb.AppendLine(string.Format(reviewFormat, "Tipas", "Data", "Atsiliepimas"));

                foreach (var review in data2)
                {
                    sb.AppendLine(string.Format(reviewFormat, review.Type, review.Date, review.Review));
                }
            }

            return sb.ToString();
        }

        private byte[] GenerateDocx(List<ReportRecord> reportData, List<ReportReview> reviewData)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    // Sukuriame pagrindinį dokumentą
                    MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                    mainPart.Document = new Document(new Body());

                    // Pridėti antraštę
                    Body body = mainPart.Document.Body;
                    body.AppendChild(new Paragraph(new Run(new Text("Mokinio ataskaita"))));

                    // Sukuriame lentelę su įvertinimais
                    var table = new Table();
                    TableProperties tblProps = new TableProperties(
                        new TableWidth { Type = TableWidthUnitValues.Auto },
                        new TableBorders(
                            new TopBorder() { Val = BorderValues.Single, Size = 4 },
                            new BottomBorder() { Val = BorderValues.Single, Size = 4 },
                            new LeftBorder() { Val = BorderValues.Single, Size = 4 },
                            new RightBorder() { Val = BorderValues.Single, Size = 4 },
                            new InsideHorizontalBorder() { Val = BorderValues.Single, Size = 4 },
                            new InsideVerticalBorder() { Val = BorderValues.Single, Size = 4 }
                        )
                    );
                    table.AppendChild(tblProps);
                    table.AppendChild(CreateTableRow("Studentas", "Klasė", "Dalykas", "Įvertinimas", "Data")); // Antraštės eilutė

                    foreach (var record in reportData)
                    {
                        table.AppendChild(CreateTableRow(record.StudentName, record.Class, record.Subject, record.Grade.ToString(), record.Date));
                    }

                    body.AppendChild(table);

                    // Jei yra atsiliepimų, įrašyti atsiliepimus
                    if (reviewData.Count > 0)
                    {
                        body.AppendChild(new Paragraph(new Run(new Text("\nAtsiliepimai:"))));
                        var reviewTable = new Table();
                        reviewTable.AppendChild(new TableProperties(
                            new TableBorders(
                                new TopBorder() { Val = BorderValues.Single, Size = 4 },
                                new BottomBorder() { Val = BorderValues.Single, Size = 4 },
                                new LeftBorder() { Val = BorderValues.Single, Size = 4 },
                                new RightBorder() { Val = BorderValues.Single, Size = 4 },
                                new InsideHorizontalBorder() { Val = BorderValues.Single, Size = 4 },
                                new InsideVerticalBorder() { Val = BorderValues.Single, Size = 4 }
                            )
                        ));
                        reviewTable.AppendChild(CreateTableRow("Tipas", "Data", "Atsiliepimas")); // Atsiliepimų antraštė

                        foreach (var review in reviewData)
                        {
                            reviewTable.AppendChild(CreateTableRow(review.Type, review.Date, review.Review));
                        }

                        body.AppendChild(reviewTable);
                    }
                }

                return memoryStream.ToArray();
            }
        }

        private TableRow CreateTableRow(params string[] columns)
        {
            TableRow tableRow = new TableRow();
            foreach (var column in columns)
            {
                TableCell cell = new TableCell(new Paragraph(new Run(new Text(column))));
                // Pridėti kraštines kiekvienai ląstelei
                cell.AppendChild(new TableCellProperties(
                    new TableCellBorders(
                        new TopBorder() { Val = BorderValues.Single, Size = 4 },
                        new BottomBorder() { Val = BorderValues.Single, Size = 4 },
                        new LeftBorder() { Val = BorderValues.Single, Size = 4 },
                        new RightBorder() { Val = BorderValues.Single, Size = 4 }
                    )
                ));
                tableRow.AppendChild(cell);
            }
            return tableRow;
        }
        public class ReportFilters
        {
            public string Student { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Subject { get; set; }
            public string GradeMin { get; set; }
            public string GradeMax { get; set; }
        }
        public class ReportRecord
        {
            public string StudentName { get; set; }
            public string Class { get; set; }
            public string Subject { get; set; }
            public int Grade { get; set; }
            public string Date { get; set; }
        }
        public class ReportReview
        {
            public string StudentName { get; set; }
            public string Type { get; set; }
            public string Date { get; set; }
            public string Review { get; set; }
        }
    }
}