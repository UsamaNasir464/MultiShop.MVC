using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
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
    public class CartHeaderConsumeApi : ICartHeaderConsumeApi
    {
        private readonly HttpClient _httpClient;
        public CartHeaderConsumeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CartCreateResponse> CreateCart(CartCreateRequest cartHeader)
        {
            CartCreateResponse newCart = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<CartCreateRequest>("api/CartHeaderApi/CreateCart", cartHeader);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCart = JsonConvert.DeserializeObject<CartCreateResponse>(result);
            }
            return newCart;
        }

        public bool DeleteCart(int id)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = _httpClient.DeleteAsync("api/CartHeaderApi/DeleteOrderById/" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<CartHeader> EditCart(CartHeader cartHeader)
        {
            CartHeader editCart = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<CartHeader>("api/CartHeaderApi/EditCart", cartHeader);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                editCart = JsonConvert.DeserializeObject<CartHeader>(result);
            }
            return editCart;
        }

        public async Task<List<CartHeader>> GetAllCart()
        {
            List<CartHeader> allCart = new List<CartHeader>();
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("api/CartHeaderApi/Index");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                allCart = JsonConvert.DeserializeObject<List<CartHeader>>(result);
            }
            return allCart;
        }

        public async Task<CartHeader> GetCartById(int id)
        {
            CartHeader cartHeader = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("api/CartHeaderApi/GetCartById" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cartHeader = JsonConvert.DeserializeObject<CartHeader>(result);
            }
            return cartHeader;
        }
    }
}
