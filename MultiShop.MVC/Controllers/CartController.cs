using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartConsumeApi _cartConsumeApi;
        public CartController(ICartConsumeApi cartConsumeApi)
        {
            _cartConsumeApi = cartConsumeApi;
        }
        public async Task<IActionResult> CartIndex()
        {
            return View(await _cartConsumeApi.LoginUserCart());
        }
        public async Task<IActionResult> Checkout()
        {
            return View(await _cartConsumeApi.LoginUserCart());
        }
        public async Task<IActionResult> Remove(int cartDetailId)
        {
            var response = await _cartConsumeApi.RemoveFromCart(cartDetailId);
            if (response)
            {
                return RedirectToAction("CartIndex");
            }
            ViewBag.ErrorMessage = "Item Not Removed From Cart";
            return View("CartIndex");
        }
        public async Task<IActionResult> AddTocart(int id, int count)
        {
            var addToCart=await _cartConsumeApi.AddToCart(id, count);
            if (addToCart)
            {
                return RedirectToAction("CartIndex"); 
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CreateCartLink()
        {
            var finalUrl = _cartConsumeApi.CreateCartUrl();
            ViewBag.finalUrl = finalUrl;
            return View();
        }
        public IActionResult CopyCart()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CartDecrypt(string urlToDecrypt)
        {
            await _cartConsumeApi.CartDecrypt(urlToDecrypt);
            return RedirectToAction("CartIndex", "Cart");
        }
    }
}
