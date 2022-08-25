using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiShop.Mvc.Utills;
using Microsoft.Extensions.Configuration;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class OrderConsumeApi :  IOrderConsumeApi
    {
        private readonly ICartConsumeApi _cartConsumeApi;
        private readonly IProductConsumeApi _productConsumeApi;
        private readonly IOrderDetailsConsuumeApi _orderDetail;
        private readonly IApiCall apiCall;
        private readonly IConfiguration config;

        public IProductConsumeApi ProductConsume { get; }

        public OrderConsumeApi(ICartConsumeApi cartConsumeApi , IProductConsumeApi productConsume , IOrderDetailsConsuumeApi orderDetail , IApiCall appiCall , IConfiguration config)
        {
            _cartConsumeApi = cartConsumeApi;
            _productConsumeApi = productConsume;
            _orderDetail = orderDetail;
            this.apiCall = appiCall;
            this.config = config;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await apiCall.CallApiGetAsync<List<Order>>(config.GetSection("ApiUrls:Order:GetAllOrder").Value);
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await apiCall.CallApiGetAsync<Order>(config.GetSection("ApiUrls:Order:GetOrderById").Value + id.ToString());
           
        }
        public async Task<CreateOrderResponse> CreateOrder(OrderCreateRequest order)
        {
            return await apiCall.CallApiPostAsync<CreateOrderResponse, OrderCreateRequest>(config.GetSection("ApiUrls:Order:CreateOrder").Value, order);
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
