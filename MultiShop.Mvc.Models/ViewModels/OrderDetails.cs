using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
        public int ProductFId { get; set; }
        [ForeignKey("ProductFId")]
        public Product Product { get; set; }
        public int OrderFId { get; set; }
        [ForeignKey("OrderFId")]
        public Order Order { get; set; }
    }
}
