using Microsoft.Extensions.Configuration;
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
    public class CategoryConsumeApi : ICategoryConsumeApi
    {
        private readonly HttpClient _httpClient;
        //private readonly IConfiguration _config;
        public CategoryConsumeApi(HttpClient httpClient/*, IConfiguration config*/)
        {
            _httpClient = httpClient;
            //_config = config;

        }
        public async Task<List<Category>> GetAllCategory()
        {
            List<Category> categoryList = new List<Category>();
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("CategoryApi");
            
           
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                categoryList = JsonConvert.DeserializeObject<List<Category>>(result);

            }
            return categoryList;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category category = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("CategoryApi?id" + id.ToString());

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(result);
            }
            
            return category;
        
        }

        public async Task<Category> CreateCategory(Category category) 
        {
            Category newCategory=null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response =  await _httpClient.PostAsJsonAsync<Category>("CategoryApi", category);
            if(response.IsSuccessStatusCode)
            {
                var result= response.Content.ReadAsStringAsync().Result;
                newCategory = JsonConvert.DeserializeObject<Category>(result);
            
            }
            return newCategory;

        }


        public async Task<Category> EditCategory(int id)
        {
            Category category = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("CategoryApi?id" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(result);
                
            }
            return category;

        }

        public bool DeleteCategory(int id)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = _httpClient.GetAsync("CategoryApi?id" + id);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }



    }
}
