using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface ICartHeaderConsumeApi
    {
        Task<List<CartHeader>> GetAllCart();
        Task<CartHeader> GetCartById(int id);
        Task<CartCreateResponse> CreateCart(CartCreateRequest cartHeader);
        Task<CartHeader> EditCart(CartHeader cartHeader);
        bool DeleteCart(int id);
    }
}
