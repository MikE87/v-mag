using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyMagazyn.Models;

namespace WirtualnyMagazyn.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepo categories = new CategoryRepo();

        //
        // GET: /Category/

        public ActionResult Index(int? id)
        {
            if ( (id != null) && categories.ValidateID(id.Value))
            {
                CategoryModel c = categories.GetCategoryByID(id.Value);
                c.selectedCategory = id.Value;
                return View(c);
            }
            else if (categories.GetAllCategories().Count() > 0)
            {
                return View(categories.GetAllCategories().First());
            }
            
            return View(new CategoryModel());
            
        }

        //
        // POST: /Category/

        [HttpPost]
        public ActionResult Index(string submitButton)
        {
            string selection = Request.Form["selectedCategory"];

            switch (submitButton)
            {
                case "Pokaż": return RedirectToAction("Index", new { id = selection });
                case "Edytuj": return RedirectToAction("Edit", new { id = selection });
                case "Usuń": return RedirectToAction("Delete", new { id = selection });
                default: return RedirectToAction("Index");
            }
        }

        public string GetCategoryDescription(int? id)
        {
            if (id != null)
            {
                if (categories.ValidateID(id.Value))
                {
                    return categories.GetCategoryByID(id.Value).Description;
                }
            }
            return "";
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View(new CategoryModel());
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categories.AddCategory(category);
                    if(Request.IsAjaxRequest())
                        return Json(new { success = true, name = category.Name, id = category.ID });
                    return RedirectToAction("Index", new { id=category.ID });
                }
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                return View(category);
            }
            catch(Exception e)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, error = e.Message });
                return View();
            }
        }
        
        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            if (categories.ValidateID(id))
            {
                return View(categories.GetCategoryByID(id));
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, CategoryModel category)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    categories.EditCategory(id, category);
                    if(Request.IsAjaxRequest())
                        return Json(new { success = true, id=id, name = category.Name });
                    return RedirectToAction("Index", new { id=id });
                }
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                return View(category);
            }
            catch(Exception e)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, error = e.Message });
                return View();
            }
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id, bool ajax = false)
        {
            try
            {
                categories.DeleteCategory(id);
                if (ajax)
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                if (ajax)
                    return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }
        }
    }
}
