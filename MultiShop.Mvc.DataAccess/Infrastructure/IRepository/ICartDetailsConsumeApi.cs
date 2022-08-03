using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.DataAccess.Infrastructure.IRepository
{
    public interface ICartDetailsConsumeApi
    {
        Task<IEnumerable<CartDetails>> GetAllCartDetails();
        Task<CartDetails> GetCartDetailsById(int id);
        Task<CartDetailsCreateResponse> CreateCartDetails(CartDetailsCreateRequest cartDetails);
        Task<CartDetailsEditResponse> EditCartDetails(CartDetailsEditRequest cartDetails);
        bool DeleteCartDetails(int id);
  
    }
}
