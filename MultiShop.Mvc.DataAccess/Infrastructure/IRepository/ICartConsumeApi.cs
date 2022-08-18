using MultiShop.Mvc.Models.ViewModels;
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
        Task<CartDto> CartUrlByUserId(string userId);
    }
}
