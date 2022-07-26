using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.Models.ViewModels
{
    [Keyless]
    public class Login
    {

        [Required(ErrorMessage = "Please Enter our Email Address"), EmailAddress]
        [Display(Name = "Email Address:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Pasword ")]
        [DataType(DataType.Password)]
        [Display(Name = "Pasword:")]
        public string Password { get; set; }
        [Display(Name = "Remember Me ")]
        public bool RememberMe { get; set; }
    }
}
