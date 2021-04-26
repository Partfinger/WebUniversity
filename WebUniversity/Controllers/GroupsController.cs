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
            db.Students.Where(student => student.GroupId != null).Load();

            ViewBag.countOfStudents = db.Groups.Select(group => group.Students.Count()).ToList();
            return View(db.Groups);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group newGroup)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(newGroup);
                db.SaveChanges();
                return RedirectToAction("Details", new { newGroup.Id });
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            Group group;
            if (FindGroupById(id, out group))
            {
                db.Courses.Where(course => course.Id == group.CourseId).FirstOrDefault();
                db.Students.Where(student => student.GroupId == group.Id).Load();
                return View(group);
            }
            return NotFound();
        }

        public IActionResult Edit(int? id)
        {
            Group group;
            if (FindGroupById(id, out group))
            {
                ViewBag.Courses = db.Courses.ToList();
                return View(group);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(Group updatedGroup)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Update(updatedGroup);
                db.SaveChanges();
                return RedirectToAction("Details", new { updatedGroup.Id });
            }
            return RedirectToAction("Edit", new { updatedGroup.Id });
        }

        public IActionResult Delete(int? id)
        {
            Group group;
            if (FindGroupById(id, out group))
            {
                if (db.Students.Where(student => student.GroupId == group.Id).Count() == 0)
                {
                    return View(group);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            Group group;
            if (FindGroupById(id, out group))
            {
                db.Groups.Remove(group);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        bool FindGroupById(int? id, out Group group)
        {
            group = null;
            if (id != null)
            {
                group = db.Groups.Find(id);
                if (group != null)
                    return true;
            }
            return false;
        }
    }
}
