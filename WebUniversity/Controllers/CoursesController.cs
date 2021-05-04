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
    public class CoursesController : BaseController<Course>
    {
        public CoursesController(UniversityContext context) : base(context)
        {
        }

        public override IActionResult Index(int page = 1)
        {
            IndexViewModel<Course> viewModel = GetItemsForPage(page);
            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            Course course;
            if (FindById(id, out course))
            {
                db.Groups.Where(group => group.CourseId == course.Id).Load();
                return View(course);
            }
            return NotFound();
        }

        public IActionResult Delete(int? id)
        {
            Course course;
            if (FindById(id, out course))
            {
                if ( db.Groups.Where(group => group.CourseId == course.Id).Count() == 0)
                {
                    return View(course);
                }
            }
            return NotFound();
        }
    }
}
