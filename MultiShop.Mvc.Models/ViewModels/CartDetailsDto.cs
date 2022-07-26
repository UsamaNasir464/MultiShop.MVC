﻿namespace MultiShop.Mvc.Models.ViewModels
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderFId { get; set; }
        public virtual CartHeaderDto CartHeader { get; set; }
        public int ProductFId { get; set; }
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
