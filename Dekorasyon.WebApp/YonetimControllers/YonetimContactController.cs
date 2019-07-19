using BLL;
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
    public class YonetimContactController : Controller
    {
        ContactManager contactManager = new ContactManager();
        // GET: Contact
        public ActionResult Index()
        {
            return View(contactManager.List());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = contactManager.Find(x => x.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
    }
}