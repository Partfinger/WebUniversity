using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            db.Groups.Load();
            return View(db.Students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student newStudent)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(newStudent);
                db.SaveChanges();
                return RedirectToAction("Details", new { newStudent.Id });
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            Student student;
            if (FindStudentById(id, out student))
            {
                student.Group = db.Groups.FirstOrDefault(Group => Group.Id == student.GroupId);
                return View(student);
            }
            return NotFound();
        }

        public IActionResult Edit(int? id)
        {
            Student student;
            if (FindStudentById(id, out student))
            {
                ViewBag.Groups = db.Groups.ToList();
                return View(student);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Update(student);
                db.SaveChanges();
                return RedirectToAction("Details", new { student.Id });
            }
            return RedirectToAction("Edit", new { student.Id });
        }

        public IActionResult Delete(int? id)
        {
            Student student;
            if (FindStudentById(id, out student))
            {
                return View(student);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Student student;
            if (FindStudentById(id, out student))
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        bool FindStudentById(int? id, out Student student)
        {
            student = null;
            if (id != null)
            {
                student = db.Students.Find(id);
                if (student != null)
                    return true;
            }
            return false;
        }
    }
}
