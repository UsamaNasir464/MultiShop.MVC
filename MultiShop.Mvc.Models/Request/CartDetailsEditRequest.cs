namespace MultiShop.Mvc.Models.Request
{
    public class CartDetailsEditRequest
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CartHeaderFId { get; set; }
        public int ProductFId { get; set; }
    }
}
