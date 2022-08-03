using Microsoft.AspNetCore.Mvc;
using MultiShop.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CartDetailsController : Controller
    {
        private readonly ICartDetailsConsumeApi _cartDetails;

        public CartDetailsController(ICartDetailsConsumeApi cartDetails)
        {
            _cartDetails = cartDetails;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var result = await _cartDetails.GetAllCartDetails();
            return View(result);
        }
     
        [HttpPost]
        public async Task<IActionResult> CreateCartHeader(CartDetailsCreateRequest cartCreateRequest)
        {
            var result = await _cartDetails.CreateCartDetails(cartCreateRequest);
            return View(result);
        }
        [HttpPut]
        public async Task<IActionResult> EditCartHeader(CartDetailsEditRequest cartEditRequest)
        {
            var result = await _cartDetails.EditCartDetails(cartEditRequest);
            return View(result);
        }
        public IActionResult DeleteCartHeader(int id)
        {
            var result = _cartDetails.DeleteCartDetails(id);
            return View(result);
        }
        public async Task<IActionResult> GetCartById(int id)
        {
            var result = await _cartDetails.GetCartDetailsById(id);
            return View();
        }
    }
}
