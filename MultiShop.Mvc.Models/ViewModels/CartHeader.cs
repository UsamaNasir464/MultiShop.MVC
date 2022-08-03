using System;
using System.ComponentModel.DataAnnotations;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class CartHeader
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int NoOfItems { get; set; }
    }
}
