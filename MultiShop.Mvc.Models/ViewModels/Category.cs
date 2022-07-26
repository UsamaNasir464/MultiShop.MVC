﻿using System.ComponentModel.DataAnnotations;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Is Required"), Display(Name = "Category Name")]
        [MaxLength(100, ErrorMessage = "Category Name is not Greater than 100")]
        public string Name { get; set; }
    }
}
