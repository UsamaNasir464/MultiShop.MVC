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
        Task<CartDto> LoginUserCart();
        Task<bool> AddToCart(int productId, int count);
        string CreateCartUrl();
        Task<bool> CartDecrypt(string urlToDecrypt);
        Task<CartDto> LoadCartDtoBasedOnRequestedUser(string decryptedUserId);
        Task<bool> AddProductToUserCartUsingUrl(int productId, int count);
    }
}
