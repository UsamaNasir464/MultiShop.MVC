using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class CartDetails
    {
        [Key]
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int CartHeaderFId { get; set; }
        [ForeignKey("CartHeaderFId")]
        public virtual CartHeader CartHeader { get; set; }
        public int ProductFId { get; set; }
        [ForeignKey("ProductFId")]
        public virtual Product Product { get; set; }
    }
}
