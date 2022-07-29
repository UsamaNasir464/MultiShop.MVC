using MultiShop.Mvc.Models.Request;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.ServiceBus.EmailService
{
    public interface IEmailSending
    {
        Task SendMessageAsync(OrderCreateRequest orderDetails, string queueName);
    }
}
