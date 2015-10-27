using BooksLibrary.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLibrary.DomainModel.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int authorId);
        Task InsertAuthor(Author author);        
        Task UpdateAuthor(Author author);
        Task DeleteAuthor(int authorId);
    }
}
