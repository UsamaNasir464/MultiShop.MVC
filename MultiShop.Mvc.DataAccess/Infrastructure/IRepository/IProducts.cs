using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
   public interface IProducts
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductsByID( int id );
        Task<Product> CreateProduct(Product product);
        Task<Product> EditProduct(Product product);
        bool DeleteProduct(int id);
    }
}
