using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class StudentsController : Controller
    {
        UniversityContext db;

        public StudentsController(UniversityContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student newStudent)
        {
            db.Students.Add(newStudent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int studentId)
        {
            return View(db.Students.Find(studentId));
        }

        [HttpPost]
        public IActionResult Update(Student student)
        {
            db.Students.Update(student);
            db.SaveChanges();
            return RedirectToAction("Details");
        }
    }
}
