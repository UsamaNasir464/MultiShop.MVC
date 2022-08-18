using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CartConsumeApi : ICartConsumeApi
    {
        private readonly HttpClient _httpClient;

        public CartConsumeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ClearCart(string userId)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            
            var response = await _httpClient.GetAsync("api/CartApi/ClearCart/" + userId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public async Task<CartDto> CreateCart(CartDto cartDto)
        {
            CartDto newCart = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<object>("api/CartApi/AddCart", cartDto);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCart = JsonConvert.DeserializeObject<CartDto>(result);
            }
            return newCart;
        }
        public async Task<CartDto> UpdateCart(CartDto cartDto)
        {
            CartDto newCart = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<CartDto>("api/CartApi/UpdateCart/", cartDto);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCart = JsonConvert.DeserializeObject<CartDto>(result);
            }
            return newCart;
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            CartDto cartDto = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/CartApi/GetCart/" + userId.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var test = JsonConvert.DeserializeObject<ResponseDto>(result);
                if (test.IsSuccess)
                {
                    cartDto = JsonConvert.DeserializeObject<CartDto>(test.Result.ToString());
                }
            }
            return cartDto;
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("/api/CartApi/RemoveCart/" + cartDetailsId.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }





        public async Task<CartDto> CartUrlByUserId(string userId)
        {
            CartDto cartDto = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/CartApi/GetCart/" + userId.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var test = JsonConvert.DeserializeObject<ResponseDto>(result);
                if (test.IsSuccess)
                {
                    cartDto = JsonConvert.DeserializeObject<CartDto>(test.Result.ToString());
                }
            }
            return cartDto;
        }


    }
}
