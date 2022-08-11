using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShop.Models.Models.DTOs
{
    public class ProductDto
    {
     
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; } 
        public decimal SalePrice { get; set; }
        public decimal? DiscountPrice { get; set; } 
        public string ProductImagePath { get; set; }
        public int CatFId { get; set; }
       public virtual Category Category { get; set; }
    }
}
