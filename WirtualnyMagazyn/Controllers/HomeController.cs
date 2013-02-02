using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyMagazyn.Models;

namespace WirtualnyMagazyn.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Category");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
