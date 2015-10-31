using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibrary.DomainModel.Models;
using System.Data.Entity;

namespace BooksLibrary.DomainModel.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        //Constructor
        public BookRepository()
        {
            this.dbContext = DbContext.Instance.Context;
        }

        //Public Methods
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await dbContext.Books.FindAsync(bookId);
        }

        public async Task<IEnumerable<Book>> SearchBooksByAuthor(string author_name)
        {
            return await (from b in dbContext.Books
                          from a in dbContext.Authors
                          where b.Author_Id == a.Author_Id
                                && (a.First_Name.ToUpper() + " " + a.Last_Name.ToUpper()) == author_name.ToUpper()
                          select b).ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksByCategory(string category_name)
        {
            return await (from b in dbContext.Books
                          from c in dbContext.Categories
                          where b.Category_Id == c.Category_Id
                                && c.Category_Name.ToUpper() == category_name.ToUpper()
                          select b).ToListAsync();
        }

        public async Task InsertBook(Book book)
        {
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(int bookId)
        {
            Book selected = dbContext.Books.Find(bookId);
            dbContext.Books.Remove(selected);
            await dbContext.SaveChangesAsync();
        }
    }
}
