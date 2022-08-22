using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace MultiShop.Mvc.DataAccess.ServiceBus.Services
{
    public class BaseService
    {
        protected readonly IConfiguration _config;
        protected readonly HttpClient _httpClient;
        public BaseService(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }
        public void CallBaseAddress()
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_config.GetConnectionString("BaseUri"));
            }
           
        }
    }
}
