using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.Mvc.Utills;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class OrderDetailsConsumeApi : IOrderDetailsConsuumeApi
    {
        private readonly IApiCall apiCall;
        private readonly IConfiguration config;
        public OrderDetailsConsumeApi(IApiCall apiCall , IConfiguration config)
        {
            this.apiCall = apiCall;
            this.config = config;
        }
        public async Task<OrderDetailsResponse> CreateOrderDetails(OrderDetailsCreateRequest request)
        {
            return await apiCall.CallApiPostAsync<OrderDetailsResponse, OrderDetailsCreateRequest>(config.GetSection("ApiUrls:OrderDetails:CreateOrderDetails").Value, request);
        }
        public async Task<List<OrderDetails>> GetAllOrderDetails()
        {
            return await apiCall.CallApiGetAsync<List<OrderDetails>>(config.GetSection("ApiUrls:OrderDetails:GetAllOrderDetails").Value);
        }
        public async Task<OrderDetails> GetOrderDetailById(int id)
        {
            return await apiCall.CallApiGetAsync<OrderDetails>(config.GetSection("ApiUrls:OrderDetails:GetOrderDetailsById").Value + id.ToString());
        }
    }
}
