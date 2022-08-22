using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MultiShop.Mvc.DataAccess.ServiceBus.Services;
using Microsoft.Extensions.Configuration;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class OrderConsumeApi : BaseService , IOrderConsumeApi
    {
        private readonly ICartConsumeApi _cartConsumeApi;
        private readonly IProductConsumeApi _productConsumeApi;
        private readonly IOrderDetailsConsuumeApi _orderDetail;
        public OrderConsumeApi(HttpClient httpClient, ICartConsumeApi cartConsumeApi,
            IProductConsumeApi productConsumeApi, IOrderDetailsConsuumeApi orderDetail
            , IConfiguration config) : base(config , httpClient)
        {
            _cartConsumeApi = cartConsumeApi;
            _productConsumeApi = productConsumeApi;
            _orderDetail = orderDetail;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> allOrders = new List<Order>();
            CallBaseAddress();
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
            CallBaseAddress();
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
            CallBaseAddress();
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
            CallBaseAddress();
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
            CallBaseAddress();
            var response = _httpClient.DeleteAsync("api/OrderApi/DeleteOrderById/" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> OrderConfirmed(CartDto orderCart)
        {
            var userId = GetEmailAndUserId.UserId;
            var response = await _cartConsumeApi.GetCartByUserId(userId);
            CartDto cartDto = new();
            cartDto.CartDetails = response.CartDetails;
            cartDto.CartHeader = response.CartHeader;
            foreach (var detail in cartDto.CartDetails)
            {
                var product = await _productConsumeApi.GetProductsById(detail.ProductFId);
                detail.Product = product;
                cartDto.CartHeader.OrderTotal += product.SalePrice * detail.Count;
            }
            OrderCreateRequest order = new();
            order.PaymentMethod = "Cod";
            order.OrderDate = System.DateTime.Now;
            order.OrderType = "Sale";
            order.GrandTotal = (cartDto.CartHeader.OrderTotal + (cartDto.CartHeader.OrderTotal / 100));
            order.Address = orderCart.Order.Address;
            order.CustomerName = orderCart.Order.CustomerName;
            order.Email = orderCart.Order.Email;
            order.PhoneNumber = orderCart.Order.PhoneNumber;
            order.UserFid = userId;
            var test = await CreateOrder(order);
            OrderDetailsCreateRequest orderdetail = new();
            foreach (var item in cartDto.CartDetails)
            {
                orderdetail.ProductFId = item.ProductFId;
                orderdetail.OrderFId = test.Order.Id;
                orderdetail.ProductQuantity = item.Count;
                orderdetail.SalePrice = item.Product.SalePrice;
                orderdetail.TotalPrice = item.Product.SalePrice * item.Count;
               await _orderDetail.CreateOrderDetails(orderdetail);
            }
            await _cartConsumeApi.ClearCart(userId);
            return true;
        }
    }
}
