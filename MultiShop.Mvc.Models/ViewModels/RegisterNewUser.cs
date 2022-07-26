using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class RegisterNewUser : IdentityUser
    {
        [Required(ErrorMessage = "Full Name Is Required"), Display(Name = "Full Name")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Password Is Required"), Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password is Not Shorter Than 8 Characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password Is Required"), Display(Name = "Confirm Password")]
        [MinLength(8, ErrorMessage = "Confirm Password is Not Shorter Than 8 Characters")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPasswrd { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }


    }
}
