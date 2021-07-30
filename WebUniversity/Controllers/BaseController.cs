using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebUniversity.ViewModels;

namespace WebUniversity.Controllers
{
    public abstract class BaseController<T> : Controller where T: class, IHaveId
    {
        protected IUnitOfWork db;
        protected const int pageSize = 25;

        public BaseController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        protected IndexViewModel<T> HandleIndex(int page = 1, string search = null)
        {
            var sourse = Filtrate(search);
            
            return Paginate(page, search, sourse);
        }

        protected IndexViewModel<T> Paginate(int page, string search, IQueryable<T> sourse)
        {
            int count = sourse.Count();
            IndexViewModel<T> viewModel;
            if (count > 0)
            {
                var itemsForPage = sourse.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                viewModel = new IndexViewModel<T>
                {
                    PageViewModel = pageViewModel,
                    Items = itemsForPage,
                    SearchData = search
                };
            }
            else
            {
                viewModel = new IndexViewModel<T>
                {
                    SearchData = search
                };
            }
            return viewModel;
        }

        public abstract IActionResult Index(int page = 1, string search = null);

        protected abstract IQueryable<T> Filtrate(string search = null);

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(T newItem)
        {
            if (ModelState.IsValid)
            {
                db.GetRepository<T>().Create(newItem);
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
                db.GetRepository<T>().Update(item);
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
                db.GetRepository<T>().Delete(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected bool FindById(int? id, out T item)
        {
            item = null;
            if (id != null)
            {
                item = db.GetRepository<T>().Find(id);
                if (item != null)
                    return true;
            }
            return false;
        }
    }
}
