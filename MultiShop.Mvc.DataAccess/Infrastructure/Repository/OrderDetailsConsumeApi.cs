using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.Services;
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
    public class OrderDetailsConsumeApi : BaseService , IOrderDetailsConsuumeApi
    {
        public OrderDetailsConsumeApi(HttpClient httpClient , IConfiguration config): base(config , httpClient)
        {
        }
        public async Task<OrderDetailsResponse> CreateOrderDetails(OrderDetailsCreateRequest request)
        {
            OrderDetailsResponse newOrderDetail = null;
            CallBaseAddress();
            var response = await _httpClient.PostAsJsonAsync<OrderDetailsCreateRequest>("api/OrderDetails/CreateOrderDetails", request);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newOrderDetail = JsonConvert.DeserializeObject<OrderDetailsResponse>(result);
            }
            return newOrderDetail;
        }
        public async Task<List<OrderDetails>> GetAllOrderDetails()
        {
            List<OrderDetails> allOrderDetails = new List<OrderDetails>();
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/OrderDetails/Index");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                allOrderDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(result);
            }
            return allOrderDetails;
        }
        public async Task<OrderDetails> GetOrderDetailById(int id)
        {
            OrderDetails orderDetails = null;
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/OrderDetails/GetOrderDetailsById/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                orderDetails = JsonConvert.DeserializeObject<OrderDetails>(result);
            }
            return orderDetails;
        }
    }
}
