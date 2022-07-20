using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface ICategoryConsumeApi
    {
        Task<List<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category category);
        public bool DeleteCategory(int id);

        Task<Category> EditCategory(int id);


    }
}
