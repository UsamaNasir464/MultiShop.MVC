using MultiShop.Mvc.Models.ViewModels;

namespace MultiShop.Mvc.Models.Request
{
    public class OrderAndCartDto
    {
        public CartDto Cart { get; set; }
        public Order Order { get; set; }
        public OrderDetails OrderDetails { get; set; }
    }
}
