using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using MultiShop.Mvc.Models.Response;

namespace MultiShop.Mvc.Utills
{
    public class ApiCall : IApiCall
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ApiCall(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<T> CallApiGetAsync<T>(string apiPath) where T : new()
        {
            T data = new T();
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            var response = await _httpClient.GetAsync(apiPath);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<T> CallApiPostAsync<T>(string apiPath,T postData) where T : new()
        {
            T data = new T();
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.PostAsJsonAsync<T>(apiPath,postData);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<T> CallApiPostAsync<T,X>(string apiPath,X postData) where T : new() where X: new()
        {
            T data = new T();
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.PostAsJsonAsync<X>(apiPath,postData);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<T>(result);
            }
            return data;
        }
        public async Task<bool> CallApiDeleteAsync(string apiPath)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            var response = await _httpClient.DeleteAsync(apiPath);
            
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<string> CallUserIdApiGetAsync(string apiPath)
        {
            string userId = string.Empty;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            var response = await _httpClient.GetAsync(apiPath);
            if (response.IsSuccessStatusCode)
            {
                userId = response.Content.ReadAsStringAsync().Result;               
            }
            return userId;
        }

        public async Task LogoutAsync(string apiPath)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            var response = await _httpClient.GetAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                GetEmailAndUserId.UserId = null;
                GetEmailAndUserId.Email = null;
            }
           
        }

        public async Task<bool> GetBoolAsync(string apiPath)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetSection("ApiUrls:ConnectionUri:BaseUri").Value);
            }
            var response = await _httpClient.GetAsync(apiPath);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
