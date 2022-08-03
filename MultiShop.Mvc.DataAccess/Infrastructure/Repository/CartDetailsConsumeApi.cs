using MultiShop.DataAccess.Infrastructure.IRepository;
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
    public class CartDetailsConsumeApi : ICartDetailsConsumeApi
    {
        private readonly HttpClient _httpClient;
        public CartDetailsConsumeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public  async Task<CartDetailsCreateResponse> CreateCartDetails(CartDetailsCreateRequest cartDetails)
        {
            CartDetailsCreateResponse newCartDetails = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<CartDetailsCreateRequest>("api/CartDetailsApi/CreateCartDetails", cartDetails);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCartDetails = JsonConvert.DeserializeObject<CartDetailsCreateResponse>(result);
            }
            return newCartDetails;
        }

        public bool DeleteCartDetails(int id)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = _httpClient.DeleteAsync("api/CartDetailsApi/DeleteCartDetails/" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<CartDetailsEditResponse> EditCartDetails(CartDetailsEditRequest cartDetails)
        {
            CartDetailsEditResponse editCartDetails = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PutAsJsonAsync<CartDetailsEditRequest>("api/CartDetailsApi/EditCartDetails", cartDetails);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                editCartDetails = JsonConvert.DeserializeObject<CartDetailsEditResponse>(result);
            }
            return editCartDetails;
        }

        public async Task<IEnumerable<CartDetails>> GetAllCartDetails()
        {
            List<CartDetails> allCart = new List<CartDetails>();
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("api/CartDetailsApi/Index");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                allCart = JsonConvert.DeserializeObject<List<CartDetails>>(result);
            }
            return allCart;
        }
     
        public async Task<CartDetails> GetCartDetailsById(int id)
        {
            CartDetails cartHeader = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("api/CartDetailsApi/GetCartDetailsById" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cartHeader = JsonConvert.DeserializeObject<CartDetails>(result);
            }
            return cartHeader;
        }

       
    }
}
