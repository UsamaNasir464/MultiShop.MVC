using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class UserAccount : IUserAccount
    {
        private readonly HttpClient _httpClient;
        public UserAccount(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            User userCreate = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<User>("api/UserAccountApi/Register", user);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                userCreate = JsonConvert.DeserializeObject<User>(display);
                GetEmailAndUserId.Email = user.Email;
                await GetUserId(GetEmailAndUserId.Email);
            }
            return userCreate;
        }
        public async Task<LoginResponse> Login(Login login)
        {
            LoginResponse loginResponse = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<Login>("api/UserAccountApi/LogIn", login);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(display);
                if (loginResponse.Email != null)
                {
                    GetEmailAndUserId.Email = loginResponse.Email;
                    await GetUserId(GetEmailAndUserId.Email);
                }
            }
            return loginResponse;
        }
        public async Task LogOut()
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var test = await _httpClient.GetAsync("api/UserAccountApi/LogOut");
            if (test.IsSuccessStatusCode)
            {
                GetEmailAndUserId.UserId = null;
                GetEmailAndUserId.Email = null;
            }
        }
        public async Task<string> GetUserId(string email)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var test = await _httpClient.GetAsync("api/UserAccountApi/GetUserId?email=" + email);
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsStringAsync().Result;
                GetEmailAndUserId.UserId = display;
                return display;
            }
            return "";
        }
    }
}