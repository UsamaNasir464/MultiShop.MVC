using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts _products;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProducts products, IWebHostEnvironment hostEnvironment)
        {
            _products = products;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _products.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> Create(ProductCreateRequest product)
        //{
        //    await _products.CreateProduct(product);
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _products.GetProductsByID(id);
            return View(result);
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
            var result = await _products.GetProductsByID(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _products.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _products.GetProductsByID(id);
            return View(result);
        }
    }

}
