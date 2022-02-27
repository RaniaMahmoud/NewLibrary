using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Library.ViewModels;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Library.Models;
using Library.Interfaces;

namespace Library.Controllers
{
    public static class  photo{
        /// <summary>
        /// Upoade photo To server
        /// Constrain: 
        ///         object Contain Image Must have proparty
        ///             1- PhotoPath  as String
        ///             2- Photo as HttpPostedFileBase
        /// </summary>
        /// <param name="ObjectConatinImage"></param>
        /// <param name="image"></param>
        public static HttpStatusCode UploadPhoto(dynamic ObjectConatinImage, HttpPostedFileBase image)
        {
            try
            {
                
                    if (image != null)
                    {
                        string fileName = Path.GetFileName(image.FileName);
                        string ServerPath = HttpContext.Current.Server.MapPath("~/Image/");
                        string imgPath = Path.Combine(ServerPath, fileName);
                        ObjectConatinImage.ImagePath = "~/Image/" + fileName;
                        ObjectConatinImage.Image.SaveAs(imgPath);
                        return HttpStatusCode.OK;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
                return HttpStatusCode.BadRequest;
            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Upoade photo To server
        /// Constrain: 
        ///         object Contain Image Must have proparty
        ///             1- PhotoPath  as String
        ///             2- Photo as HttpPostedFileBase
        /// </summary>
        /// <param name="ObjectConatinFile"></param>
        /// <param name="File"></param>
        public static HttpStatusCode UploadFile(dynamic ObjectConatinFile, HttpPostedFileBase File)
        {
            try
            {

                if (File != null)
                {
                    string fileName = Path.GetFileName(File.FileName);
                    string ServerPath = HttpContext.Current.Server.MapPath("/Content/File/");
                    string filePath = Path.Combine(ServerPath, fileName);
                    ObjectConatinFile.FilePath = "/Content/File/" + fileName;
                    ObjectConatinFile.File.SaveAs(filePath);
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
                return HttpStatusCode.BadRequest;
            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

    }
    public class BooksController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //[Authorize]
        public ActionResult AllBook()
        {
            TempData.Keep("userName");

            var books = context.Books.Include(b => b.Department).Include(b => b.Publisher).Include(b => b.User);
            return View(books.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(book);
        }

        public ActionResult AddNewBook()
        {
            //ViewData["dept"] = context.Departments.ToList();
            //ViewData["pub"] = context.Publishers.ToList();
            ViewData["usr"] = context.Users.ToList();
            TempData.Keep("userName");
            ViewModel ViewModelM = new ViewModel();
            List<Publisher> publishers = context.Publishers.ToList();
            List<Department> departments  = context.Departments.ToList();

            IEnumerable<SelectListItem> selPublisher = from c in publishers
                                                       select new SelectListItem
                                                       {
                                                           Text = c.Name,
                                                           Value = c.ID.ToString()
                                                       };
            IEnumerable<SelectListItem> selDepartment = from c in departments
                                                       select new SelectListItem
                                                       {
                                                           Text = c.Name,
                                                           Value = c.ID.ToString()
                                                       };
            ViewModelM.Publishers = selPublisher;
            ViewModelM.Departments = selDepartment;
            /*
            ViewBag.DeptID = new SelectList(context.Departments, "ID", "Name");
            ViewBag.PublID = new SelectList(context.Publishers, "ID", "Name");
            ViewBag.UserID = new SelectList(context.Users, "ID", "Name");
            */
            return View(ViewModelM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult AddNewBook(Book book)
        {
            Book BookDB = context.Books.FirstOrDefault(b => b.ID == book.ID);
            

            ViewModel ViewModelM = new ViewModel();
            List<Publisher> publishers = context.Publishers.ToList();
            List<Department> departments = context.Departments.ToList();

            IEnumerable<SelectListItem> selPublisher = from c in publishers
                                                       select new SelectListItem
                                                        {
                                                            Text = c.Name,
                                                            Value = c.ID.ToString()
                                                        };
            IEnumerable<SelectListItem> selDepartment = from c in departments
                                                        select new SelectListItem
                                                        {
                                                            Text = c.Name,
                                                            Value = c.ID.ToString()
                                                        };
            ViewModelM.Publishers = selPublisher;
            ViewModelM.Departments = selDepartment;
            if (ModelState.IsValid)
            {

                //check whether name is already exists in the database or not
                bool nameAlreadyExists = context.Books.Any(b => b.Title == book.Title);


                if (nameAlreadyExists)
                {
                    //adding error message to ModelState
                    ModelState.AddModelError("Title", "Book Name Already Exists.");
//                    ViewData["dept"] = context.Departments.ToList();
                    ViewData["usr"] = context.Users.ToList();
                    //                    ViewData["pub"] = context.Publishers.ToList();

                    ViewModelM.Book = book;
                    ViewModelM.Publishers = selPublisher;
                    ViewModelM.Departments = selDepartment;
                    return View(ViewModelM);
                }
                //if (book.Image != null && book.Image.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(book.Image.FileName);
                //    string imgPath = Path.Combine(Server.MapPath("~/Image/"), fileName);
                //    book.ImagePath = "~/Image/" + fileName;
                //    book.Image.SaveAs(imgPath);

                //}
                photo.UploadPhoto(book, book.Image);
                photo.UploadFile(book, book.File);
                context.Books.Add(book);
                context.SaveChanges();
                Author author = new Author();
                author.Name = book.AuthorName;
                author.BookID = book.ID;
                author.Book = book;
                context.Authors.Add(author);
                context.SaveChanges();
                Book BoDB = context.Books.FirstOrDefault(b => b.ID == book.ID);
                BoDB.Authors.Add(context.Authors.FirstOrDefault(b => b.ID == author.ID));
                context.SaveChanges();
                ViewModelM.Book = book;
                return RedirectToAction("AllBook");
            }
            //ViewData["dept"] = context.Departments.ToList();
            ViewData["usr"] = context.Users.ToList();
            //ViewData["pub"] = context.Publishers.ToList();
            TempData.Keep("userName");

            //ViewBag.DeptID = new SelectList(context.Departments, "ID", "Name", book.Department.ID);
            //ViewBag.PublID = new SelectList(context.Publishers, "ID", "Name", book.Publisher.ID);
            return View(ViewModelM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewData["dept"] = context.Departments.ToList();
            ViewData["pub"] = context.Publishers.ToList();
            TempData.Keep("userName");

            //ViewBag.UserID = new SelectList(context.Users, "ID", "Name", book.UserID);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            Book BookDB = context.Books.FirstOrDefault(b => b.ID == id);
            if (!ModelState.IsValid)
            {
                ViewData["dept"] = context.Departments.ToList();
                ViewData["pub"] = context.Publishers.ToList();
                return View(book);

            }
            else
            { //checking model state

                //check whether name is already exists in the database or not
                bool nameAlreadyExists = context.Books.Any(b => b.Title == book.Title);


                if (nameAlreadyExists)
                {
                    //adding error message to ModelState
                    ModelState.AddModelError("Title", "Book Name Already Exists.");
                    ViewData["dept"] = context.Departments.ToList();
                    ViewData["pub"] = context.Publishers.ToList();
                    return View(book);
                }
            }

            if (ModelState.IsValid)
            {
                /*if (book.Image != null && book.Image.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(book.Image.FileName);
                    string imgPath = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    book.ImagePath = "~/Image/" + fileName;
                    book.Image.SaveAs(imgPath);
                }
                */
                //context.Authors.Add(author);

                BookDB.Title = book.Title;
                BookDB.DepartmentID = book.DepartmentID;
                BookDB.PublisherID = book.PublisherID;
                BookDB.Image = book.Image;
                BookDB.ImagePath = book.ImagePath;
                BookDB.Yeare = book.Yeare;
                BookDB.ISBN = book.ISBN;
                BookDB.AuthorName = book.AuthorName;
                BookDB.Authors = book.Authors;

                Author author = context.Authors.FirstOrDefault(a => a.BookID == book.ID);
                author.Name = BookDB.AuthorName;
                author.BookID = BookDB.ID;

                //context.Books.Add(book);
                context.SaveChanges();
                TempData.Keep("userName");

                return RedirectToAction("AllBook");
            }
            ViewData["dept"] = context.Departments.ToList();
            ViewData["pub"] = context.Publishers.ToList();
            // ViewBag.UserID = new SelectList(context.Users, "ID", "Name", book.UserID);
            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            TempData.Keep("userName");

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TempData.Keep("userName");

            Book book = context.Books.Find(id);
            Author author = context.Authors.FirstOrDefault(b => b.BookID == book.ID);
            context.Books.Remove(book);
            context.Authors.Remove(author);
            context.SaveChanges();

            return RedirectToAction("AllBook");
        }
        public ActionResult Borrow(int? id)
        {
            TempData.Keep("user");
            TempData.Keep("userName");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            //User user = context.Users.Find(userID);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrowConfirmed(int id)
        {
            TempData.Keep("user");
            TempData.Keep("userName");

            if (TempData.ContainsKey("user"))
            {
                UserA userTD = TempData["user"] as UserA;
                Message mes = new Message();
                UserA user = context.UserAs.FirstOrDefault(u => u.ID == userTD.ID);
                mes.UserID = user.ID;
                mes.User = new UserA()
                {
                    Name = user.Name,
                    ID = user.ID,
                    Email = user.Email,
                    Password = user.Password,
                    Books = user.Books
                };
                mes.User = user;
                //mes.time = DateTime.Today.Date;
                mes.Text = "YouAreBorrowTheBook";
                user.Messages.Add(mes);
                context.Entry(mes).State = EntityState.Added;
                context.SaveChanges();

                return RedirectToAction("Page", "Books", new { messageid = mes.ID, bookid = id });
            }
            return View();
        }

        //public ActionResult Page2(User user)
        //{

        //    //using (var context2 = new DataContext())
        //    //{
        //    //    Book bookbd = context2.Books.FirstOrDefault(b => b.ID == book.ID);
        //    //    User user = context2.Users.FirstOrDefault(u => u.ID == 1);
        //    //    book.User = user;
        //    //    book.UserID = user.ID;
        //    //    //context2.Entry(book).State = EntityState.Modified;
        //    //    user.Books.Add(book);
        //    //    bookbd = book;
        //    //    //context2.Entry(bookbd).State = EntityState.Modified;
        //    //    //context2.SaveChanges();
        //    //}

        //    return View(user);
        //}
        public ActionResult Page(int messageid,int bookid)
        {
            TempData.Keep("user");
            TempData.Keep("userName");

            Message message = context.Messages.FirstOrDefault(m => m.ID == messageid);
            TempData["UserMessage"] = message;
            UserA user = context.UserAs.FirstOrDefault(u => u.ID == message.UserID);
            //message.User = user;
            Book book = context.Books.FirstOrDefault(b => b.ID == bookid);
            book.UserID = user.ID;
            book.User = user;
            user.Books.Add(book);

            context.SaveChanges();

            return View(message);
        }

        public ActionResult Remove(int id)
        {
            TempData.Keep("UserMessage");
            TempData.Keep("user");
            TempData.Keep("userName");

            UserA user = TempData["user"] as UserA;
            Book book = context.Books.FirstOrDefault(b => b.ID == id);
            Message message = TempData["UserMessage"] as Message;
            book.UserID = null;
            book.User = null;
            user.Books.Remove(book);
            user.Messages.Remove(message);
            //context.Messages.Remove(message);

            context.SaveChanges();
            ViewData["Mess"] = "You Are Remove The Book";
            TempData.Keep("user");

            return RedirectToAction("BorrowsBook",new { id = user.ID });
        }

        public ActionResult BorrowsBook(int id)
        {
            TempData.Keep("user");
            TempData.Keep("UserMessage");
            TempData.Keep("userName");

            //Message message = context.Messages.FirstOrDefault(m => m.ID == id);
            var books = context.Books.Where(b => b.UserID == id);
            //Book book = context.Books.FirstOrDefault(b => b.ID == bookid);
            //Message message = context.Messages.FirstOrDefault(m => m.ID == messageid);
            //book.UserID = null;
            //book.User = null;
            //user.Books.Remove(book);
            //context.Messages.Remove(message);

            //context.SaveChanges();
            //ViewData["Mess"] = "You Are Remove The Book";
            return View(books);
        }
    }
}