using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryConsumeApi _consumeCategory;
        public CategoryController(ICategoryConsumeApi consumeCategory)
        {
            _consumeCategory = consumeCategory;

        }
        public async Task<ActionResult> Index()
        {
            
              List<Category> allCategories=   await _consumeCategory.GetAllCategory();
            
            
            return View(allCategories);
        }
    }
}
