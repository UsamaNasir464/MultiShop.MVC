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
    
    }
}
