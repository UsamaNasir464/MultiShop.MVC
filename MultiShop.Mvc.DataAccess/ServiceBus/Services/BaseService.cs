using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.ServiceBus.Services
{
    public class BaseService
    {
        protected readonly IConfiguration _config;

        public BaseService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> CallApiAsync(string path)
        {
            string response = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.GetConnectionString("BaseUri"));
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ApiResponse = await client.GetAsync(path);
                if (ApiResponse.IsSuccessStatusCode)
                {
                    response = ApiResponse.Content.ReadAsStringAsync().Result;
                }
                return response;
            }
        }




    }
}
