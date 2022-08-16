using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.MVC.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProducts _products;
        private readonly ICartConsumeApi _cartConsumeApi;

        public HomeController(IProducts products, ILogger<HomeController> logger, ICartConsumeApi cartConsumeApi)
        {
            _products = products;
            _logger = logger;
            _cartConsumeApi = cartConsumeApi;
        }
        
      
        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAllProducts());
        }
       
        public async Task<IActionResult> Cart(int id, int count)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = GetEmailAndUserId.UserId
                }
            };
            CartDetailsDto cartDetailsDto = new CartDetailsDto
            {
                Count = count,
                ProductFId = id
            };
            var productResponse = await _products.GetProductsByID(id);
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetailsDto);
            cartDetailsDto.Product = productResponse;
            cartDto.CartDetails = cartDetailsDtos;

            var addToCart = await _cartConsumeApi.CreateCart(cartDto);
            if (addToCart != null)
            {
                return RedirectToAction("CartIndex", "Cart");
            }



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
