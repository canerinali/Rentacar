using BLL;
using BLL.Results;
using Dekorasyon.WebApp.Filters;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dekorasyon.WebApp.Models;

namespace Dekorasyon.WebApp.YonetimControllers
{
    [Auth]
    [Exc]
    public class YonetimAboutController : Controller
    {
        AboutManager aboutManager = new AboutManager();
        // GET: About
        public ActionResult Index()
        {
            return View(aboutManager.List());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(About about, HttpPostedFileBase PostImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedName");

            if (ModelState.IsValid)
            {
                if (PostImage != null &&
                   (PostImage.ContentType == "image/jpeg" ||
                   PostImage.ContentType == "image/jpg" ||
                   PostImage.ContentType == "image/png"))
                {
                    string filename = $"about_{about.Title.Trim()}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    about.AboutImage = filename;
                }

                aboutManager.Insert(about);
                return RedirectToAction("Index");
            }
            BusinessLayerResult<About> res = aboutManager.InsertAboutFoto(about);

            return View(about);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = aboutManager.Find(x => x.Id == id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(About about, HttpPostedFileBase PostImage)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedName");

            if (ModelState.IsValid)
            {
                if (PostImage != null &&
                   (PostImage.ContentType == "image/jpeg" ||
                   PostImage.ContentType == "image/jpg" ||
                   PostImage.ContentType == "image/png"))
                {
                    string filename = $"about_{about.Id}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    about.AboutImage = filename;
                }
                BusinessLayerResult<About> res = aboutManager.UpdateAboutFoto(about);

                return RedirectToAction("Index");
            }
            return View(about);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = aboutManager.Find(x => x.Id == id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = aboutManager.Find(x => x.Id == id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = aboutManager.Find(x => x.Id == id);
            aboutManager.Delete(about);
            return RedirectToAction("Index");
        }
    }
}