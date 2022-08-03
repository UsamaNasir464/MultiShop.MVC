using System;

namespace MultiShop.Mvc.Models.Request
{
    public class CartCreateRequest
    {
        public Guid UserId { get; set; }
        public int NoOfItems { get; set; }
    }
}
