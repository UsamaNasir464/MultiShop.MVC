using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IOrderConsumeApi
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<CreateOrderResponse> CreateOrder(OrderCreateRequest order);
        Task<bool> OrderConfirmed(CartDto orderCart);
    }
}
