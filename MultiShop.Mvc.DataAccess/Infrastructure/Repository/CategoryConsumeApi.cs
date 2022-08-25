using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.Mvc.Utills;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CategoryConsumeApi : ICategoryConsumeApi
    {
        private readonly IApiCall apiCall;
        private readonly IConfiguration config;

        public CategoryConsumeApi( IApiCall apiCall, IConfiguration config)
        {
            this.apiCall = apiCall;
            this.config = config;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await apiCall.CallApiGetAsync<List<Category>>(config.GetSection("ApiUrls:Category:GetAllCategory").Value);
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await apiCall.CallApiGetAsync<Category>(config.GetSection("ApiUrls:Category:GetCategoryById").Value + id.ToString());
        }
        public async Task<Category> CreateCategory(Category category)
        {
            return await apiCall.CallApiPostAsync<Category>(config.GetSection("ApiUrls:Category:CreateCategory").Value, category);
        }
        public async Task<Category> EditCategory(Category category)
        {
            return await apiCall.CallApiPostAsync<Category>(config.GetSection("ApiUrls:Category:EditCategory").Value, category);
        }
        public async Task<bool> DeleteCategory(int id)
        {
            return await apiCall.CallApiDeleteAsync(config.GetSection("ApiUrls:Category:DeleteCategory").Value + id.ToString());
        }
    }
}
