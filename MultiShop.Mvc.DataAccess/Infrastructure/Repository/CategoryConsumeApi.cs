using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.Services;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CategoryConsumeApi : BaseService, ICategoryConsumeApi 
    {
        private readonly HttpClient _httpClient;

        public CategoryConsumeApi(HttpClient httpClient, IConfiguration config) : base(config)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            List<Category> categoryList = new List<Category>();
            var response = await CallApiAsync(_config.GetConnectionString("GetAllCategory"));
            if (!string.IsNullOrEmpty(response))
            {
                categoryList = JsonConvert.DeserializeObject<List<Category>>(response);
            }
            return categoryList;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category category = null;
            var response = await CallApiAsync(_config.GetConnectionString("GetCategoryById") + id.ToString());
            if (!string.IsNullOrEmpty(response))
            {
                category = JsonConvert.DeserializeObject<Category>(response);
            }
            return category;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            Category newCategory = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<Category>("api/CategoryApi/CreateCategory/", category);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCategory = JsonConvert.DeserializeObject<Category>(result);
            }
            return newCategory;
        }


        public async Task<Category> EditCategory(Category category)
        {
            Category updateCategory = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.PostAsJsonAsync<Category>("api/CategoryApi/UpdateCategory", category);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(result);
            }
            return updateCategory;
        }

        public bool DeleteCategory(int id)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = _httpClient.DeleteAsync("api/CategoryApi/DeleteCategoryById/" + id.ToString());
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
