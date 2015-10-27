using BooksLibrary.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.DomainModel.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task InsertCategory(Category category);
        Task DeleteCategory(int categoryId);
        Task UpdateCategory(Category category);
    }
}
