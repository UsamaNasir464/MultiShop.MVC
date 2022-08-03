namespace MultiShop.Mvc.Models.Request
{
    public class CartDetailsCreateRequest
    {
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CartHeaderFId { get; set; }
        public int ProductFId { get; set; }
    }
}
