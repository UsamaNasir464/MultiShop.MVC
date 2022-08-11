using System.Collections.Generic;
namespace MultiShop.Mvc.Models.ViewModels
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails{ get; set; }
    }
}
