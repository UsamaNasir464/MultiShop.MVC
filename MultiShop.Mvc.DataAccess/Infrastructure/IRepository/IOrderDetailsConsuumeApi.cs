using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.IRepository
{
    public interface IOrderDetailsConsuumeApi
    {
        Task<List<OrderDetails>> GetAllOrderDetails();
        Task<OrderDetails> GetOrderDetailById(int id);
        Task<OrderDetailsResponse> CreateOrderDetails(OrderDetailsCreateRequest request);
    }
}
