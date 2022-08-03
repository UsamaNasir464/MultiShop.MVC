using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IProducts
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductsByID( int id );
        //Task<Product> CreateProduct(ProductCreateRequest product);
        Task<ProductEditRequest> EditProduct(ProductEditRequest product);
        bool DeleteProduct(int id);
    }
}
