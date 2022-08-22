using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductConsumeApi _products;
        public ProductController(IProductConsumeApi products)
        {
            _products = products;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAllProducts());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCreateRequest product)
        {
            _products.CreateProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _products.GetProductsById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditRequest product)
        {
            await _products.EditProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _products.GetProductsById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _products.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _products.GetProductsById(id));
        }
    }
}
