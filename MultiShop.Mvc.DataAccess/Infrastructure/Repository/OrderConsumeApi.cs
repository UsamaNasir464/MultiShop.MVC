using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class OrderConsumeApi : IOrderConsumeApi
    {
        private readonly HttpClient _httpClient;
        public OrderConsumeApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> allOrders = new List<Order>();
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/OrderApi/Index");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                allOrders = JsonConvert.DeserializeObject<List<Order>>(result);
            }
            return allOrders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/OrderApi/GetOrderById/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(result);
            }
            return order;
        }

        public async Task<CreateOrderResponse> CreateOrder(OrderCreateRequest order)
        {
            CreateOrderResponse neworder = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<OrderCreateRequest>("api/OrderApi/CreateOrder", order);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                neworder = JsonConvert.DeserializeObject<CreateOrderResponse>(result);
            }
            return neworder;
        }


        public async Task<OrderEditRequest> EditOrder(OrderEditRequest order)
        {
            OrderEditRequest editOrder = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<OrderEditRequest>("api/OrderApi/EditOrder", order);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                editOrder = JsonConvert.DeserializeObject<OrderEditRequest>(result);
            }
            return editOrder;
        }


        public bool DeleteOrder(int id)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = _httpClient.DeleteAsync("api/OrderApi/DeleteOrderById/" + id.ToString());
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
