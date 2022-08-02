using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.MVC.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProducts _products;

        public HomeController(IProducts products, ILogger<HomeController> logger)
        {
            _products = products;
            _logger = logger;
        }
        
      
        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAllProducts());
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
