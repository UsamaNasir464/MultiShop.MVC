using MultiShop.Mvc.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.ServiceBus.EmailService
{
    public interface IEmailSending
    {
        Task SendMessageAsync(OrderCreateRequest orderDetails, string queueName);
    }
}
