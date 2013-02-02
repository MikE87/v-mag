using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WirtualnyMagazyn.Models;

namespace WirtualnyMagazyn.Controllers
{
    public class FileController : Controller
    {
        FileRepo files = new FileRepo();

        //
        // GET: /Product/GetImage/5

        public FileContentResult GetImage(int id)
        {
            var fileList = files.GetFiles(id);
            if (fileList.Count() > 0)
                return File(fileList.ElementAt(0).Data.ToArray(),
                            fileList.ElementAt(0).ContentType);
            else
                return null;
        }

        //
        // GET: /Product/SetImage/5

        public ActionResult SetImage(int id)
        {

            if (files.ValidateProductID(id))
            {
                FileModel file = new FileModel();
                file.ProductID = id;
                return View(file);
            }
            return RedirectToAction("Index", "Category");
        }

        //
        // POST: /Product/SetImage/5

        [HttpPost]
        public ActionResult SetImage(int id, FileModel photo)
        {
            try
            {
                photo.ProductID = id;

                if (Request.IsAjaxRequest())
                {
                    photo.Data = Request.BinaryRead(Request.ContentLength);
                    photo.Name = Request.Headers.Get("X-File-Name");
                    photo.ContentType = Request.ContentType;

                    string error = photo.Validate;
                    if (error == null)
                    {
                        files.AddFile(photo);
                        return Json(new { success = true });
                    }

                    return Json(new { errors = error });
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        photo.Name = photo.file.FileName;
                        photo.ContentType = photo.file.ContentType;
                        byte[] tmp = new byte[photo.file.ContentLength];
                        photo.file.InputStream.Read(tmp, 0, photo.file.ContentLength);
                        photo.Data = tmp;

                        files.AddFile(photo);
                        return RedirectToAction("Details", "Product", new { id = id });
                    }

                    return View(photo);
                }
            }
            catch (Exception e)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new { success = false, error = e.Message });
                }
                else
                {
                    ViewData["ERROR"] = e.Message;
                    return View();
                }
            }
        }

        //
        // GET: /File/Delete/5
 
        public ActionResult DeleteImage(int id, bool ajax = false)
        {
            if (files.ValidateImageID(id))
            {
                files.DeleteFile(id);
                if (ajax)
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Details", "Product", new { id=id });
            }
            if (ajax)
                return Json(new { success = false });
            return RedirectToAction("Index", "Category");
        }
    }
}
