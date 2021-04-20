using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models;

namespace WebUniversity.Controllers
{
    public class GroupsController : Controller
    {
        UniversityContext db;

        public GroupsController(UniversityContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            db.Courses.Load();
            return View(db.Groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group newGroup)
        {
            db.Groups.Add(newGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Group group = db.Groups.Find(id);
                if (group != null)
                {
                    db.Courses.Where(course => course.Id == group.CourseId).FirstOrDefault();
                    db.Students.Where(student => student.GroupId == group.Id).Load();
                    return View(group);
                }
            }
            return RedirectToAction("Error");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Courses = db.Courses.ToList();
            return View(db.Groups.Find(id));
        }

        [HttpPost]
        public IActionResult Update(Group updatedGroup)
        {
            db.Groups.Update(updatedGroup);
            db.SaveChanges();
            return RedirectToAction("Details", new { updatedGroup.Id });
        }
    }
}
