using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.Models.Request;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.ServiceBus.EmailService
{
    public class EmailSending : IEmailSending
    {
        private readonly IConfiguration _config;
        public EmailSending(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendMessageAsync(OrderCreateRequest orderDetails, string queueName)
        {
            var conectionString = _config.GetConnectionString("AzureServiceBusConnectionString");
            var qClient = new QueueClient(conectionString, queueName);
            var msgBody = System.Text.Json.JsonSerializer.Serialize(orderDetails);
            var msg = new Message(Encoding.UTF8.GetBytes(msgBody));
            await qClient.SendAsync(msg);
        }
    }
}
