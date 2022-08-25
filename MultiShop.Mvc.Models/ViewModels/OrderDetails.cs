namespace MultiShop.Mvc.Models.ViewModels
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
        public int ProductFId { get; set; }
        public Product Product { get; set; }
        public int OrderFId { get; set; }
        public Order Order { get; set; }
    }
}
