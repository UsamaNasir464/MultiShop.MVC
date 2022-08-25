using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.Mvc.Utills;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class UserAccount : IUserAccount
    {
        private readonly IApiCall apiCall;
        private readonly IConfiguration config;

        public UserAccount(IApiCall apiCall, IConfiguration config)
        {
            this.apiCall = apiCall;
            this.config = config;
        }
        public async Task<User> CreateUserAsync(User user)
        {
           var data =  await apiCall.CallApiPostAsync<User>(config.GetSection("ApiUrls:UserAccount:Registration").Value, user);
            GetEmailAndUserId.Email = data.Email;

           await GetUserId(GetEmailAndUserId.Email);
            return data;

        }
        public async Task<LoginResponse> Login(Login login)
        {
            var data = await apiCall.CallApiPostAsync<LoginResponse,Login>(config.GetSection("ApiUrls:UserAccount:Login").Value, login);
            GetEmailAndUserId.Email = data.Email;
            await GetUserId(GetEmailAndUserId.Email);
            return data;
        }
        public async Task LogOut()
        {
            await apiCall.LogoutAsync(config.GetSection("ApiUrls:UserAccount:Logout").Value);
        }
        public async Task<string> GetUserId(string email)
        {
           var userId= await apiCall.CallUserIdApiGetAsync(config.GetSection("ApiUrls:UserAccount:GetUserId").Value + email);
           return GetEmailAndUserId.UserId = userId;
           
        }
    }
}