using BooksLibrary.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.DomainModel.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int bookId);
        Task InsertBook(Book book);
        Task DeleteBook(int bookId);
        Task UpdateBook(Book book);        
        Task<IEnumerable<Book>> GetBooksByCategory(string category);
        Task<IEnumerable<Book>> GetBooksByAuthor(string author);
    }
}
