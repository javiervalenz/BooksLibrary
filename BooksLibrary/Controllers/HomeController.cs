using BooksLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksLibrary.Controllers
{
    public class HomeController : Controller
    {
        private BooksLibraryEntities db = new BooksLibraryEntities();

        private List<SelectListItem> GetAuthors(bool FirstItemOption = true)
        {
            List<SelectListItem> authorsList = new List<SelectListItem>();

            if (FirstItemOption)
            {
                SelectListItem firstItem = new SelectListItem();
                firstItem.Text = "All";
                firstItem.Value = "0";
                authorsList.Add(firstItem);
            }

            foreach (Author a in db.Authors.OrderBy(a => a.First_Name + a.Last_Name))
            {
                SelectListItem item = new SelectListItem();
                item.Text = a.First_Name + " " + a.Last_Name;
                item.Value = a.Author_Id.ToString();
                authorsList.Add(item);
            }

            return authorsList;
        }

        private List<SelectListItem> GetCategories(bool FirstItemOption = true)
        {
            List<SelectListItem> categoryList = new List<SelectListItem>();

            if (FirstItemOption)
            {
                SelectListItem firstItem = new SelectListItem();
                firstItem.Text = "All";
                firstItem.Value = "0";
                categoryList.Add(firstItem);
            }

            foreach (Category c in db.Categories.OrderBy(c => c.Category_Name))
            {
                SelectListItem item = new SelectListItem();
                item.Text = c.Category_Name;
                item.Value = c.Category_Id.ToString();
                categoryList.Add(item);
            }

            return categoryList;
        }

        public ActionResult Index()
        {
            return View();
        }        

        public ActionResult ListBooks()
        {
            ViewBag.Message = "Displaying All Books";           

            return View(db.Books);
        }

        public ActionResult ListByAuthor()
        {
            ViewBag.Message = "Displaying Books by Author";

            ViewData["Authors"] = GetAuthors();            

            return View(db.Books);
        }

        [HttpPost]
        public ActionResult ListByAuthor(int id)
        {
            if(id > 0)
            {
                var filteredBooksList = db.Books.Where(b => b.Author_Id == id);
                return PartialView("PartialBookList", filteredBooksList);
            }

            return PartialView("PartialBookList", db.Books);
        }

        public ActionResult ListByCategory()
        {
            ViewBag.Message = "Displaying Books by Category";

            ViewData["Categories"] = GetCategories();

            return View(db.Books);
        }

        [HttpPost]
        public ActionResult ListByCategory(int id)
        {
            if (id > 0)
            {
                var filteredBooksList = db.Books.Where(b => b.Category_Id == id);
                return PartialView("PartialBookList", filteredBooksList);
            }

            return PartialView("PartialBookList", db.Books);
        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Message = "Edit Book";

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return View("Error");
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            ViewBag.Message = "Edit Book";

            if (ModelState.IsValid)
            {                
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListBooks");
            }

            return View(book);
        }

        public ActionResult Create()
        {   
            ViewData["Categories"] = GetCategories(false);

            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                Author author = db.Authors.FirstOrDefault(a => a.First_Name.ToUpper() == book.Author.First_Name.ToUpper() 
                                                        && a.Last_Name.ToUpper() == book.Author.Last_Name.ToUpper());
                if (author != null)
                    book.Author = author;
               
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("ListBooks");
            }

            return View(book);
        }
        
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Message = "Delete Book";

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return View("Error");
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Message = "Delete Book";

            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction("ListBooks");
        }
    }
}