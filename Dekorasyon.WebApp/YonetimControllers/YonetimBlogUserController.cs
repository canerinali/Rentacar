using BLL;
using BLL.Results;
using Dekorasyon.WebApp.Models;
using Dekorasyon.WebApp.ViewModels;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dekorasyon.WebApp.YonetimControllers
{
    public class YonetimBlogUserController : Controller
    {
        // GET: BlogUser
        private BlogUserManager blogUserManager = new BlogUserManager();


        public ActionResult Index()
        {
            return View(blogUserManager.List());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser bloguser = blogUserManager.Find(x => x.Id == id.Value);

            if (bloguser == null)
            {
                return HttpNotFound();
            }

            return View(bloguser);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogUser blogUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedName");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> res = blogUserManager.Insert(blogUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(blogUser);
                }

                return RedirectToAction("Index");
            }

            return View(blogUser);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser blogUser = blogUserManager.Find(x => x.Id == id.Value);

            if (blogUser == null)
            {
                return HttpNotFound();
            }

            return View(blogUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogUser blogUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedName");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<BlogUser> res = blogUserManager.Update(blogUser);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(blogUser);
                }

                return RedirectToAction("Index");
            }
            return View(blogUser);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogUser blogUser = blogUserManager.Find(x => x.Id == id.Value);

            if (blogUser == null)
            {
                return HttpNotFound();
            }

            return View(blogUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogUser blogUser = blogUserManager.Find(x => x.Id == id);
            blogUserManager.DeleteUser(blogUser);


            return RedirectToAction("Index");
        }
    }
}