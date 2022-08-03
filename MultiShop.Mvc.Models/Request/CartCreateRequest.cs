using System;

namespace MultiShop.Mvc.Models.Request
{
    public class CartCreateRequest
    {
        public string UserId { get; set; }
        public int NoOfItems { get; set; }
    }
}
