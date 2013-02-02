using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyMagazyn.Models;
using System.Web.UI.WebControls;

namespace WirtualnyMagazyn.Controllers
{
    public class ProductController : Controller
    {
        ProductRepo products = new ProductRepo();

        //
        // GET: /Product/

        public ActionResult Index(int? id)
        {
            if (id == null)
                return null;
            CategoryRepo cr = new CategoryRepo();
            if(cr.ValidateID(id.Value))
            {
                ViewData["category"] = id;
                return View(products.GetProductsFromCategory(id.Value));
            }
            return RedirectToAction("Index", "Category");
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            if(products.ValidateID(id))
                return View(products.GetProductByID(id));
            return RedirectToAction("Index", "Category");
        }

        //
        // GET: /Product/Create

        public ActionResult Create(int id)
        {
            CategoryRepo cr = new CategoryRepo();
            if (cr.ValidateID(id))
            {
                ProductModel p = new ProductModel();
                p.Category = id;
                return View(p);
            }
            return RedirectToAction("Index", "Category");
        } 

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(int id, ProductModel product)
        {
            try
            {
                product.Category = id;
                if (ModelState.IsValid)
                {
                    products.AddProduct(product);
                    if(Request.IsAjaxRequest())
                        return Json(new { success = true });
                    return RedirectToAction("Index", "Category", new { id=id });
                }
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                return View(product);
            }
            catch (Exception e)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, error = e.Message });
                return View();
            }
        }
        
        //
        // GET: /Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            if(products.ValidateID(id))
                return View(products.GetProductByID(id));
            return RedirectToAction("Index", "Category");
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProductModel product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    products.EditProduct(id, product);
                    if (Request.IsAjaxRequest())
                        return Json(new { success = true });
                    return RedirectToAction("Details", "Product", new { id=id });
                }
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                product.ID = id;
                return View(product);
            }
            catch(Exception e)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, error = e.Message });
                return View();
            }
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id, int? category, bool ajax = false)
        {
            try
            {
                products.DeleteProduct(id);
                if (ajax)
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "Category", new { id=category.Value });
            }
            catch(Exception e)
            {
                if (ajax)
                    return Json(new { success = false, error = e.Message }, JsonRequestBehavior.AllowGet);
                return View();
            }
        }
    }
}
