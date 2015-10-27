using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibrary.DomainModel.Models;
using System.Data.Entity;

namespace BooksLibrary.DomainModel.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        //Constructor
        public AuthorRepository()
        {
            this.dbContext = DbContext.Instance.Context;
        }

        //Public Methods
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int authorId)
        {
            return await dbContext.Authors.FindAsync(authorId);
        }

        public async Task InsertAuthor(Author author)
        {
            dbContext.Authors.Add(author);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAuthor(Author author)
        {
            dbContext.Entry(author).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int authorId)
        {
            Author selected = dbContext.Authors.Find(authorId);
            dbContext.Authors.Remove(selected);
            await dbContext.SaveChangesAsync();
        }        
        
    }
}
