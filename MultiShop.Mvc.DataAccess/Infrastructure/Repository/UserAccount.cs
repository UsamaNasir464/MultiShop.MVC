using Microsoft.AspNetCore.Identity;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<User>("api/UserAccountApi/Register", user);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                userCreate = JsonConvert.DeserializeObject<User>(display);
            }
            return userCreate;
        }
      
        public async Task<Login> Login(Login login)
        {
            Login loginUser = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<Login>("api/UserAccountApi/LogIn", login);
            return loginUser;
        }

        public  Task LogOut()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response =  _httpClient.GetAsync("api/UserAccountApi/LogOut");
            return response;
        }
    }
}

