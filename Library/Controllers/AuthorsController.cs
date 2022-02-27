using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstContext;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            TempData.Keep("userName");

            var authors = context.Authors.Include(a => a.Book);
            return View(authors.ToList());
        }

        // GET: Authors/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Author author = db.Authors.Find(id);
        //    if (author == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(author);
        //}

        // GET: Authors/Create
        public ActionResult Create()
        {
            ViewData["book"] = context.Books.ToList();
            TempData.Keep("userName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BookID")] Author author)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["book"] = context.Books.ToList();
            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewData["book"] = context.Books.ToList();
            TempData.Keep("userName");

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BookID")] Author author)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Entry(author).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["book"] = context.Books.ToList();
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = context.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData.Keep("userName");

            Author author = context.Authors.Find(id);
            context.Authors.Remove(author);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
