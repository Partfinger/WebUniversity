using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using System;
using System.Linq;
using WebUniversity.ViewModels;

namespace WebUniversity.Controllers
{
    public class GroupsController : BaseController<Group>
    {
        public GroupsController(IUnitOfWork context) : base(context)
        {
        }

        [Breadcrumb("Groups")]
        public override IActionResult Index(int page = 1, string search = null)
        {
            IndexViewModel<Group> viewModel = HandleIndex(page, search);
            db.GetRepository<Course>().GetAll().Load();
            db.GetRepository<Student>().GetAll().Where(student => student.GroupId != null).Load();
            
            ViewBag.countOfStudents = db.GetRepository<Group>().GetAll().Select(group => group.Students.Count()).ToList();

            return View(viewModel);
        }

        [Breadcrumb("Edit")]
        public override IActionResult Edit(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                ViewBag.Courses = db.GetRepository<Course>().GetAll().ToList();
                return View(group);
            }
            return NotFound();
        }

        [Breadcrumb("Detail")]
        public IActionResult Details(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                db.GetRepository<Course>().GetAll().Where(course => course.Id == group.CourseId).FirstOrDefault();
                db.GetRepository<Student>().GetAll().Where(student => student.GroupId == group.Id).Load();
                return View(group);
            }
            return NotFound();
        }

        [Breadcrumb("Delete")]
        public IActionResult Delete(int? id)
        {
            Group group;
            if (FindById(id, out group))
            {
                if (db.GetRepository<Student>().GetAll().Where(student => student.GroupId == group.Id).Count() == 0)
                {
                    return View(group);
                }
            }
            return NotFound();
        }

        protected override IQueryable<Group> Filtrate(string search = null)
        {
            IQueryable<Group> groups;
            if (!string.IsNullOrEmpty(search))
            {
                groups = db.GetRepository<Group>().GetAll().Where(p => p.Name.Contains(search));
            }
            else
            {
                groups = db.GetRepository<Group>().GetAll();
            }
            return groups;
        }
    }
}
