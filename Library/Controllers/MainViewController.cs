using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class MainViewController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //public ActionResult LogIn()
        //{
        //        return View();
        //}
        //[HttpPost]
        //public ActionResult LogIn(Admin user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var userDB = context.Admins.FirstOrDefault(u => u.Id == user.Id);
        //    if(userDB== null)
        //    {
        //        return View(user);
        //    }
        //    if (user.Email == userDB.Email && user.Password == userDB.Password)
        //    {
        //        return RedirectToAction("AllBook", "Books");

        //    }
        //    else
        //    {
        //        return View(user);
        //    }
        //}

        public ActionResult StartPage()
        {
            TempData.Keep("userName");

            return View();
        }
        public ActionResult UserView()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("UserView");
        }
    }
}