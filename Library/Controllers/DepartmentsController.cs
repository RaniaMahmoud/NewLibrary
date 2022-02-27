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
    public class DepartmentsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Departments
        public ActionResult Index()
        {
            TempData.Keep("userName");

            return View(context.Departments.ToList());
        }

        // GET: Departments/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Department department = context.Departments.Find(id);
        //    if (department == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(department);
        //}

        // GET: Departments/Create
        public ActionResult Create()
        {
            TempData.Keep("userName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Departments.Add(department);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = context.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep("userName");

                context.Entry(department).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = context.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData.Keep("userName");

            Department department = context.Departments.Find(id);
            context.Departments.Remove(department);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AllDepartment()
        {
            //User user = context.Users.FirstOrDefault(u => u.ID == userID);
            TempData.Keep("user");
            TempData.Keep("userName");

            return View(context.Departments.ToList());
        }
        public ActionResult AllBookInDept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = context.Departments.Find(id);

            TempData.Keep("user");
            TempData.Keep("userName");

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);

        }
        public ActionResult BookDetails(int? id)
        {
            TempData.Keep("user");
            TempData.Keep("userName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //public ActionResult Borrow(int? id)
        //{
        //    TempData.Keep("user");
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    if (TempData.ContainsKey("user"))
        //    {
        //        Book book = context.Books.Find(id);
        //        User user = TempData["user"] as User;
        //        Message message = new Message();
        //        message.User = user;
        //        message.Text = "Dear " + (message.User.Name).ToString() + " you borrow this book at " + (DateTime.Now.Date).ToString();
        //        if (book == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        context.Messages.Add(message);
        //        context.SaveChanges();
        //    }
        //    return View();
        //}

    }
}
