namespace MultiShop.Mvc.Models.Request
{
    public class OrderDetailsCreateRequest
    {
        public int ProductQuantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductFId { get; set; }
        public int OrderFId { get; set; }
    }
}
