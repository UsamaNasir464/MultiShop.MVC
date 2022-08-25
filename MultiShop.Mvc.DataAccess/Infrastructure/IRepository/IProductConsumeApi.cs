using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IProductConsumeApi
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductsById(int id);
        Product CreateProduct(ProductCreateRequest product);
        Task<ProductEditRequest> EditProduct(ProductEditRequest product);
        Task<bool> DeleteProduct(int id);
    }
}
