using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IOrderConsumeApi
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<OrderCreateRequest> CreateOrder(OrderCreateRequest order);
        bool DeleteOrder(int id);
        Task<OrderEditRequest> EditOrder(OrderEditRequest order);
    }
}
