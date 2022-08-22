using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
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
            List<Category> allCategories = await _consumeCategory.GetAllCategory();
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
            return View(await _consumeCategory.GetCategoryById(id));
        }
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _consumeCategory.GetCategoryById(id));
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
            return View(await _consumeCategory.GetCategoryById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _consumeCategory.DeleteCategory(id);
            return RedirectToAction("index");
        }
    }
}
