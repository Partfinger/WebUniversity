using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.Models;
using WebUniversity.Models.ViewModels;

namespace WebUniversity.Controllers
{
    public class StudentsController : BaseController<Student>
    {
        public StudentsController(UniversityContext context) : base(context)
        {
        }

        [Breadcrumb("Students")]
        public override IActionResult Index(int page = 1)
        {
            IndexViewModel<Student> viewModel = GetItemsForPage(page);
            db.Groups.Load();
            return View(viewModel);
        }

        [Breadcrumb("Details")]
        public IActionResult Details(int? id)
        {
            Student student;
            if (FindById(id, out student))
            {
                student.Group = db.Groups.FirstOrDefault(Group => Group.Id == student.GroupId);
                return View(student);
            }
            return NotFound();
        }

        [Breadcrumb("Edit")]
        public override IActionResult Edit(int? id)
        {
            Student student;
            if (FindById(id, out student))
            {
                ViewBag.Groups = db.Groups.ToList();
                return View(student);
            }
            return NotFound();
        }

        [Breadcrumb("Delete")]
        public IActionResult Delete(int? id)
        {
            Student student;
            if (FindById(id, out student))
            {
                return View(student);
            }
            return NotFound();
        }
    }
}
