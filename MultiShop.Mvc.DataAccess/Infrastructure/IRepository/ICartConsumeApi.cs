using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
   public interface ICartConsumeApi
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> CreateCart(CartDto cartDto);
        Task<CartDto> UpdateCart(CartDto cartDto);
        Task<bool>RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
    }
}
