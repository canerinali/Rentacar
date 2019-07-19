using BLL;
using BLL.Results;
using Dekorasyon.WebApp.Filters;
using Dekorasyon.WebApp.Models;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dekorasyon.WebApp.YonetimControllers
{
    public class YonetimPostController : Controller
    {
        private PostManager postManager = new PostManager();
        private CategoryManager categoryManager = new CategoryManager();

        [Auth]
        public ActionResult Index()
        {
            var notes = postManager.ListQueryable().Include("Category").Include("Owner").OrderByDescending(
                x => x.ModifiedOn);

            return View(notes.ToList());
        }
        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }
        [Auth]
        [HttpPost]
        public ActionResult Create(Post post, HttpPostedFileBase PostImage)
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
                    string filename = $"post_{post.Title.Trim()}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    post.PostImageFilename = filename;
                }
                post.Owner = CurrentSession.User;
                postManager.Insert(post);
                return RedirectToAction("Index");
            }
            BusinessLayerResult<Post> res = postManager.InsertPostFoto(post);

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", post.CategoryId);
            return View(post);
        }
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postManager.Find(x => x.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", post.CategoryId);
            return View(post);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post, HttpPostedFileBase PostImage)
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
                    string filename = $"post_{post.Id}.{PostImage.ContentType.Split('/')[1]}";

                    PostImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    post.PostImageFilename = filename;
                }
                BusinessLayerResult<Post> res = postManager.UpdatePostFoto(post);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", post.CategoryId);
            return View(post);
        }
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postManager.Find(x => x.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postManager.Find(x => x.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postManager.Find(x => x.Id == id);
            postManager.Delete(post);
            return RedirectToAction("Index");
        }
    }
}