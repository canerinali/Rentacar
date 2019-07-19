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

namespace Dekorasyon.WebApp.YonetimControllers
{
    [Auth]
    [Exc]
    public class YonetimBannerController : Controller
    {
        HomeManager homeManager = new HomeManager();
        // GET: BlogHome
        public ActionResult Index()
        {
            return View(homeManager.List());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Home home, HttpPostedFileBase PostImage)
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
                    string filename = $"banner_{home.Title.Trim()}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    home.HomeImage = filename;
                }
                homeManager.Insert(home);
                return RedirectToAction("Index");
            }
            BusinessLayerResult<Home> res = homeManager.InsertPostFoto(home);

            return View(home);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = homeManager.Find(x => x.Id == id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Home home, HttpPostedFileBase PostImage)
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
                    string filename = $"post_{home.Id}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    home.HomeImage = filename;
                }
                BusinessLayerResult<Home> res = homeManager.UpdatePostFoto(home);

                return RedirectToAction("Index");
            }
            return View(home);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = homeManager.Find(x => x.Id == id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = homeManager.Find(x => x.Id == id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Home home = homeManager.Find(x => x.Id == id);
            homeManager.Delete(home);
            return RedirectToAction("Index");
        }
    }
}