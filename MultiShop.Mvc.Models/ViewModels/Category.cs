using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage = "Category Name Is Required"), Display(Name = "Category Name")]
        [MaxLength(100, ErrorMessage = "Category Name is not Greater than 100")]
        public string Name { get; set; }
    }
}
