using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IUserAccount
    {
        Task<User> CreateUserAsync(User user);
        Task<LoginResponse> Login(Login login);
        Task LogOut();
        Task<string> GetUserId(string email);
    }
}
