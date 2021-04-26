using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class CoursesController : Controller
    {
        UniversityContext db;

        public CoursesController(UniversityContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Courses);
        }

        public IActionResult Details(int? id)
        {
            Course course;
            if (FindCourseById(id, out course))
            {
                db.Groups.Where(group => group.CourseId == course.Id).Load();
                return View(course);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course newCourse)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(newCourse);
                db.SaveChanges();
                return RedirectToAction("Details", new { newCourse.Id });
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            Course course;
            if (FindCourseById(id, out course))
            {
                return View(course);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Update(course);
                db.SaveChanges();
                return RedirectToAction("Details", new { course.Id });
            }
            return RedirectToAction("Edit", new { course.Id });
        }

        public IActionResult Delete(int? id)
        {
            Course course;
            if (FindCourseById(id, out course))
            {
                if ( db.Groups.Where(group => group.CourseId == course.Id).Count() == 0)
                {
                    return View(course);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Course course;
            if (FindCourseById(id, out course))
            {
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        bool FindCourseById(int? id, out Course course)
        {
            course = null;
            if (id != null)
            {
                course = db.Courses.Find(id);
                if (course != null)
                    return true;
            }
            return false;
        }
    }
}
