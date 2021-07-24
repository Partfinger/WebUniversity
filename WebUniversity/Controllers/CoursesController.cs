using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.DataAccess;
using WebUniversity.Models;
using WebUniversity.Models.ViewModels;

namespace WebUniversity.Controllers
{
    public class CoursesController : BaseController<Course>
    {
        public CoursesController(IUnitOfWork context) : base(context)
        {
        }

        [Breadcrumb("Courses")]
        public override IActionResult Index(int page = 1, string search = null)
        {
            IndexViewModel<Course> viewModel = Paginate(page, search);

            return View(viewModel);
        }

        [Breadcrumb("Details")]
        public IActionResult Details(int? id)
        {
            Course course;
            if (FindById(id, out course))
            {
                db.GetRepository<Group>().GetAll().Where(group => group.CourseId == course.Id).Load();
                return View(course);
            }
            return NotFound();
        }

        [Breadcrumb("Delete")]
        public IActionResult Delete(int? id)
        {
            Course course;
            if (FindById(id, out course))
            {
                if (db.GetRepository<Group>().GetAll().Where(group => group.CourseId == course.Id).Count() == 0)
                {
                    return View(course);
                }
            }
            return NotFound();
        }

        protected override IQueryable<Course> Filtrate(string search = null)
        {
            IQueryable<Course> courses;
            if (!string.IsNullOrEmpty(search))
            {
                courses = db.GetRepository<Course>().GetAll().Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }
            else
            {
                courses = db.GetRepository<Course>().GetAll();
            }
            return courses;
        }
    }
}
