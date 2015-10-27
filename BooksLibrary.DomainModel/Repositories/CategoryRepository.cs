using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksLibrary.DomainModel.Models;
using System.Data.Entity;

namespace BooksLibrary.DomainModel.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        //Constructor
        public CategoryRepository()
        {
            this.dbContext = DbContext.Instance.Context;
        }

        //Public Methods
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await dbContext.Categories.FindAsync(categoryId);
        }

        public async Task InsertCategory(Category category)
        {
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            dbContext.Entry(category).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            Category selected = dbContext.Categories.Find(categoryId);
            dbContext.Categories.Remove(selected);
            await dbContext.SaveChangesAsync();
        }
    }
}
