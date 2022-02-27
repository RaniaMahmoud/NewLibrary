using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class PublishersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Publishers
        public ActionResult Index()
        {
            TempData.Keep("userName");

            return View(context.Publishers.ToList());
        }

        // GET: Publishers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Publisher publisher = context.Publishers.Find(id);
        //    if (publisher == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(publisher);
        //}

        // GET: Publishers/Create
        public ActionResult Create()
        {
            TempData.Keep("userName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Publishers.Add(publisher);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = context.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Entry(publisher).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = context.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData.Keep("userName");

            Publisher publisher = context.Publishers.Find(id);
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
