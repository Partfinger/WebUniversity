using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models;
using WebUniversity.Models.ViewModels;

namespace WebUniversity.Controllers
{
    public class GroupsController : BaseController<Group>
    {
        public GroupsController(UniversityContext context) : base(context)
        {
        }

        public override IActionResult Index(int page = 1)
        {
            IndexViewModel<Group> viewModel = GetItemsForPage(page);
            db.Courses.Load();
            db.Students.Where(student => student.GroupId != null).Load();
            
            ViewBag.countOfStudents = db.Groups.Select(group => group.Students.Count()).ToList();
            return View(viewModel);
        }

        public override IActionResult Edit(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                ViewBag.Courses = db.Courses.ToList();
                return View(group);
            }
            return NotFound();
        }

        public IActionResult Details(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                db.Courses.Where(course => course.Id == group.CourseId).FirstOrDefault();
                db.Students.Where(student => student.GroupId == group.Id).Load();
                return View(group);
            }
            return NotFound();
        }

        public IActionResult Delete(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                if (db.Students.Where(student => student.GroupId == group.Id).Count() == 0)
                {
                    return View(group);
                }
            }
            return NotFound();
        }
    }
}
