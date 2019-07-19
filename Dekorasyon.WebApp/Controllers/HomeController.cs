using BLL;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dekorasyon.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private PostManager postManager = new PostManager();
        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View(postManager.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Category cat = categoryManager.Find(x => x.Id == id.Value);

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home");
            //}

            //List<Note> notes = cat.Notes.Where(
            //    x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()

            List<Post> posts = postManager.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("Index", posts);
        }
        [Route("Arac-{id}")]
        public ActionResult PostDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Post> posts = postManager.ListQueryable().Where(
                    x => x.IsDraft == false && x.Id == id).ToList();

            return View("PostDetail", posts);
        }
        public ActionResult PostDetailRecent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category cat = categoryManager.Find(x => x.Id == id.Value);

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home");
            //}

            //List<Note> notes = cat.Notes.Where(
            //    x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()
            List<Post> posts = postManager.ListQueryable().Where(
                x => x.IsDraft == false && x.Id == id).OrderByDescending(x => x.ModifiedOn).ToList();

            return PartialView("_PartialPostDetailRecentPost", posts);
        }
    }
}