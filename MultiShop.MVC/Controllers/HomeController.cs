using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.MVC.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductConsumeApi _products;
        public HomeController(IProductConsumeApi products)
        {
            _products = products;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAllProducts());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
