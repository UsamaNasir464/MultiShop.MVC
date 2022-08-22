using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.Services;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CategoryConsumeApi : BaseService , ICategoryConsumeApi
    {
        public CategoryConsumeApi(IConfiguration config , HttpClient httpClient) : base(config , httpClient)
        {
        }
        public async Task<List<Category>> GetAllCategory()
        {
            List<Category> categoryList = new List<Category>();
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/CategoryApi/Index");
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
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/CategoryApi/GetCategoryById/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<Category>(result);
            }
            return category;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            Category newCategory = null;
            CallBaseAddress();
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
            CallBaseAddress();
            var response = await _httpClient.PostAsJsonAsync<Category>("api/CategoryApi/UpdateCategory", category);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                updateCategory = JsonConvert.DeserializeObject<Category>(result);
            }
            return updateCategory;
        }
        public bool DeleteCategory(int id)
        {
            CallBaseAddress();
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
