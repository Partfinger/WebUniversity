using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUniversity.Controllers
{
    public class HomeController : Controller
    {
        [DefaultBreadcrumb("Main")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
