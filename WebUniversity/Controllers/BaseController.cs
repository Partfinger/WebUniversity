using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUniversity.DataAccess;
using WebUniversity.Models.Interfaces;
using WebUniversity.Models.ViewModels;

namespace WebUniversity.Controllers
{
    public abstract class BaseController<T> : Controller where T: class, IHaveId
    {
        protected UniversityContext db;
        protected const int pageSize = 25;

        public BaseController(UniversityContext context)
        {
            db = context;
        }

        protected IndexViewModel<T> GetItemsForPage(int page = 1)
        {
            var sourse = db.Set<T>().ToList();
            int count = sourse.Count();
            var itemsForPage = sourse.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<T> viewModel = new IndexViewModel<T>
            {
                PageViewModel = pageViewModel,
                Items = itemsForPage
            };
            return viewModel;
        }

        public abstract IActionResult Index(int page = 1);

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(T newItem)
        {
            if (ModelState.IsValid)
            {
                db.Set<T>().Add(newItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public virtual IActionResult Edit(int? id)
        {
            T item;
            if (FindById(id, out item))
            {
                return View(item);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Update(T item)
        {
            if (ModelState.IsValid)
            {
                db.Set<T>().Update(item);
                db.SaveChanges();
                return RedirectToAction("Details", new { item.Id });
            }
            return RedirectToAction("Edit", new { item.Id });
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int? id)
        {
            T item;
            if (FindById(id, out item))
            {
                db.Set<T>().Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected bool FindById(int? id, out T item)
        {
            item = null;
            if (id != null)
            {
                item = db.Set<T>().Find(id);
                if (item != null)
                    return true;
            }
            return false;
        }
    }
}
