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
            if(id != null)
            {
                Course course = db.Courses.Find(id);
                db.Groups.Where(group => group.CourseId == id).Load();
                if (course != null)
                    return View(course);
            }
            return RedirectToAction("Error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course newCourse)
        {
            db.Courses.Add(newCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                Course course = db.Courses.Find(id);
                if (course != null)
                    return View(course);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult Update(Course course)
        {
            db.Courses.Update(course);
            db.SaveChanges();
            return RedirectToAction("Details", new { course.Id });
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
