using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;


using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class Products : IProducts
    {
        private readonly HttpClient _httpClient;

        public Products( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await  _httpClient.GetAsync("ProductApi");
            if (response.IsSuccessStatusCode)
            {
                var display =  response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(display);

            }
            return products;
        }
    }
}
