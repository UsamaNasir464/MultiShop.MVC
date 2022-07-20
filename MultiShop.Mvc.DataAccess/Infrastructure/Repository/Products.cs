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
    public class Products : IProducts
    {
        private readonly HttpClient _httpClient;

        public Products( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            Product productsCreate = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<Product>("ProductApi", product);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                productsCreate = JsonConvert.DeserializeObject<Product>(display);

            }
            return productsCreate;


        }

        public   bool DeleteProduct(int id)
        {
         
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response =  _httpClient.DeleteAsync("ProductApi?id" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false ;
        }

        public async  Task<Product> EditProduct(Product product)
        {
            Product productsEdit = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PutAsJsonAsync<Product>("ProductApi", product);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                productsEdit = JsonConvert.DeserializeObject<Product>(display);

            }
            return productsEdit;
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

        public  async Task<Product> GetProductsByID(int id)
        {
            Product product = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("ProductApi?id" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
              var display =   response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(display);
            }
            return product;
        }
        
    }

}
