using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Admin Name Is Required"), Display(Name = "Admin Name")]
        
        public string  Name { get; set; }


        [Required(ErrorMessage = "Email Is Required"), Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Is Required"), Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password is Not Shorter Than 8 Characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password Is Required"), Display(Name = "Confirm Password")]
        [MinLength(8, ErrorMessage = "Confirm Password is Not Shorter Than 8 Characters")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPasswrd { get; set; }

        [Display(Name ="Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required"), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }



    }
}
