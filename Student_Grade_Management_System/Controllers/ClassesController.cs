﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Add()
        {
            ViewBag.Teachers = _context.Teachers.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(int classNumber, string classLetter, int studentCount, string Teacher_Username)
        {
            var newClass = new Class
            {
                Number = classNumber,
                Letter = classLetter,
                StudentCount = studentCount,
                Teacher_Username = Teacher_Username
            };

            var classes = await _context.Classes.ToListAsync();
            foreach (var c in classes)
            {
                if (c.Number == newClass.Number && c.Letter == newClass.Letter)
                {
                    return View("Error", new ErrorViewModel { RequestId = "Tokia klasė jau egzistuoja." });
                }
            }

            await _context.Classes.AddAsync(newClass);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            // Code to create a new schedule for a class
            return View();
        }
    }
}
