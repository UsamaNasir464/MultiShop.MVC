using System;
using System.ComponentModel.DataAnnotations;

namespace MultiShop.Mvc.Models.Request
{
    public class OrderCreateRequest
    {
        [Required(ErrorMessage = "Email Address Is Required"), Display(Name = "Email Address")]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Customer Name Is Required"), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required"), Display(Name = "Phone #")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address Is Required"), Display(Name = "Address")]
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public string UserFid { get; set; }
    }
}
