using BooksLibrary.DomainModel.Models;
using BooksLibrary.DomainModel.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BooksLibrary.Controllers
{
    public class HomeController : Controller
    {
        private BookRepository bookDb = new BookRepository();
        private AuthorRepository authorDb = new AuthorRepository();
        private CategoryRepository categoryDb = new CategoryRepository();

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

            foreach (var a in authorDb.GetAllAuthors().Result.OrderBy(a => a.First_Name + a.Last_Name))
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

            foreach (var c in categoryDb.GetAllCategories().Result.OrderBy(c => c.Category_Name))
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

        public async Task<ActionResult> ListBooks()
        {
            ViewBag.Message = "Displaying All Books";
            var model = await bookDb.GetAllBooks();
            return View(model);
        }

        public ActionResult ListByAuthor()
        {
            ViewBag.Message = "Displaying Books by Author";

            ViewData["Authors"] = GetAuthors();            

            return View(bookDb.GetAllBooks().Result);
        }

        [HttpPost]
        public ActionResult ListByAuthor(int id)
        {
            if(id > 0)
            {
                var filteredBooksList = bookDb.GetAllBooks().Result.Where(b => b.Author_Id == id);
                return PartialView("PartialBookList", filteredBooksList);
            }

            return PartialView("PartialBookList", bookDb.GetAllBooks().Result);
        }

        public ActionResult ListByCategory()
        {
            ViewBag.Message = "Displaying Books by Category";

            ViewData["Categories"] = GetCategories();

            return View(bookDb.GetAllBooks().Result);
        }

        [HttpPost]
        public ActionResult ListByCategory(int id)
        {
            if (id > 0)
            {
                var filteredBooksList = bookDb.GetAllBooks().Result.Where(b => b.Category_Id == id);
                return PartialView("PartialBookList", filteredBooksList);
            }

            return PartialView("PartialBookList", bookDb.GetAllBooks().Result);
        }

        public async Task<ActionResult> Edit(int id = 0)
        {
            ViewBag.Message = "Edit Book";

            var book = await bookDb.GetBookById(id);
            if (book == null)
            {
                return View("Error");
            }

            return View(book);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Book book)
        {
            ViewBag.Message = "Edit Book";

            if (ModelState.IsValid)
            {
                await bookDb.UpdateBook(book);            
                
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
        public async Task<ActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                //If author already exists
                Author author = authorDb.GetAllAuthors().Result.FirstOrDefault(a => a.First_Name.ToUpper() == book.Author.First_Name.ToUpper() 
                                                        && a.Last_Name.ToUpper() == book.Author.Last_Name.ToUpper());
                //then do not create new author, use existing instead
                if (author != null)
                    book.Author = author;

                await bookDb.InsertBook(book);
                
                return RedirectToAction("ListBooks");
            }

            return View(book);
        }
        
        public async Task<ActionResult> Delete(int id = 0)
        {
            ViewBag.Message = "Delete Book";

            Book book = await bookDb.GetBookById(id);
            if (book == null)
            {
                return View("Error");
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Message = "Delete Book";

            await bookDb.DeleteBook(id);

            return RedirectToAction("ListBooks");
        }
    }
}