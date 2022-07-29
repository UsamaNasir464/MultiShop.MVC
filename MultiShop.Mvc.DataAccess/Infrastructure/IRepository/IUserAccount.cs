using Microsoft.AspNetCore.Identity;
using MultiShop.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IUserAccount
    {
        Task<User> CreateUserAsync(User user);
        Task<Login>Login(Login login);
        Task LogOut();
    }
}
