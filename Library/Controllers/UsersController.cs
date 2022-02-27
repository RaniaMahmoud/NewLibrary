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
    public class UsersController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(context.UserAs.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserA user = context.UserAs.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: Users/Create
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserA user)
        {
            if (ModelState.IsValid)
            {
                var UserInDB = context.UserAs.FirstOrDefault(u => u.Email == user.Email);
                if (UserInDB == null)
                {
                    ViewData["Error"] = "Enter you Data Correct";

                    //return RedirectToAction("Create", "Users", new { bookid = id });
                }
                else if (UserInDB.Name== "Admin1" && user.Email == "UserAdmin_1@gmail.com" && user.Password == "123")
                {
                    TempData["user"] = UserInDB;
                    TempData["userName"] = UserInDB.Name;

                    return RedirectToAction("StartPage", "MainView");
                }
                else if (UserInDB.Email == user.Email && UserInDB.Password == user.Password)
                {
                    TempData["user"] = UserInDB;
                    TempData["userName"] = UserInDB.Name;
                    return RedirectToAction("AllDepartment", "Departments");
                }
                else if (UserInDB.Email == user.Email && UserInDB.Password != user.Password)
                {
                    ViewData["Error"] = "Enter you Data Correct";
                    return View(user);
                }
                else
                {
                    //context.Users.Add(user);
                    //context.SaveChanges();
                    ViewData["Error"] = "Enter you Data Correct";
                    return View(user);
                }
            }

            return View(user);
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserA user)
        {
            if (ModelState.IsValid)
            {
                bool nameAlreadyExists = context.UserAs.Any(u => u.Email == user.Email);
                if (nameAlreadyExists)
                {
                    ModelState.AddModelError("Email", "Email Already Exists.");
                    return View(user);
                }
                else
                {
                    context.UserAs.Add(user);
                    context.SaveChanges();
                    TempData["user"] = user;
                    TempData["userName"] = user.Name;
                    return RedirectToAction("AllDepartment", "Departments");
                }

            }

            return View(user);
        }

        //public ActionResult Enter()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Enter(User user, int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var UserInDB = context.Users.FirstOrDefault(u => u.Email == user.Email);
        //        if (UserInDB == null)
        //        {
        //            return RedirectToAction("Create", "Users", new { bookid = id});
        //        }
        //        else if (UserInDB.Email==user.Email && UserInDB.Password == user.Password)
        //        {
        //            return RedirectToAction("Borrow", "Books", new { Bookid = id, userID = UserInDB.ID });
        //        }else if(UserInDB.Email != user.Email && UserInDB.Password == user.Password)
        //        {
        //            ViewData["Error"] = "Enter you Data Correct";
        //            return View(user);
        //        }
        //        else if (UserInDB.Email == user.Email && UserInDB.Password != user.Password)
        //        {
        //            ViewData["Error"] = "Enter you Data Correct";
        //            return View(user);
        //        }
        //        else
        //        {
        //            //context.Users.Add(user);
        //            //context.SaveChanges();
        //            return RedirectToAction("Create", "Users", new { bookid = id});
        //        }
        //    }

        //    return View(user);
        //}
        //// GET: Users/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(User user,int bookid)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool nameAlreadyExists = context.Users.Any(u => u.Email == user.Email);
        //        if (nameAlreadyExists)
        //        {
        //            ModelState.AddModelError("Email", "Email Already Exists.");
        //            return View(user);
        //        }
        //        else
        //        {
        //            context.Users.Add(user);
        //            context.SaveChanges();
        //            return RedirectToAction("Borrow", "Books", new { Bookid = bookid, userID = user.ID });
        //        }

        //    }

        //    return View(user);
        //}

        // GET: Users/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserA user = context.UserAs.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Password")] UserA user)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserA user = context.UserAs.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserA user = context.UserAs.Find(id);
            context.UserAs.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
