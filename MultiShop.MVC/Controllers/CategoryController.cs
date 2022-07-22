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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _consumeCategory.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            var result = await _consumeCategory.GetCategoryById(id);
            return View(result);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _consumeCategory.GetCategoryById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _consumeCategory.EditCategory(category);
                return RedirectToAction("index");
            }
           
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _consumeCategory.GetCategoryById(id);
            return View(result);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirm(int id)
        {
            _consumeCategory.DeleteCategory(id);
            return RedirectToAction("index");
        }




    }
}
