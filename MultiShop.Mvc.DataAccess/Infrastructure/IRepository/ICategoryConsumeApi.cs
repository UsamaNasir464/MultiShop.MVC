using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface ICategoryConsumeApi
    {
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<Category> EditCategory(Category category);
    }
}
