using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartConsumeApi _cartConsumeApi;
        private readonly IProducts _productConsumeApi;

        public CartController(ICartConsumeApi cartConsumeApi, IProducts productConsumeApi)
        {
            _cartConsumeApi = cartConsumeApi;
            _productConsumeApi = productConsumeApi;

        }

        public async Task<IActionResult> CartIndex()
        {
            
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailId)
        {
            var userId = GetEmailAndUserId.UserId;
            var response = await _cartConsumeApi.RemoveFromCart(cartDetailId);
            if (response)
            {
                return RedirectToAction("CartIndex");
            }
            return View();
        }



        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = GetEmailAndUserId.UserId;
            var response = await _cartConsumeApi.GetCartByUserId(userId);

            CartDto cartDto = new();
            

            if (response != null)
            {
                cartDto.CartDetails = response.CartDetails;
                cartDto.CartHeader = response.CartHeader;
                if (cartDto?.CartHeader != null)
                {
                    foreach (var detail in cartDto.CartDetails)
                    {
                        var product = await _productConsumeApi.GetProductsByID(detail.ProductFId);
                        detail.Product = product;
                        cartDto.CartHeader.OrderTotal += product.SalePrice * detail.Count;
                    }
                }
            }
            return cartDto;
        }




        //public async Task<IActionResult> index(int id,int count)
        //{
        //    CartDto cartDto = new()
        //    {
        //        CartHeader = new CartHeaderDto
        //        {
        //            UserId = GetEmailAndUserId.UserId
        //        }
        //    };
        //    CartDetailsDto cartDetailsDto = new CartDetailsDto
        //    {
        //        Count = count,
        //        ProductFId = id
        //    };
        //    var productResponse = await _productConsumeApi.GetProductsByID(id);
        //    List<CartDetailsDto> cartDetailsDtos = new();
        //    cartDetailsDtos.Add(cartDetailsDto);
        //    cartDto.CartDetails = cartDetailsDtos;

        //    var addToCart = await _cartConsumeApi.CreateCart(cartDto);
        //    if (addToCart != null)
        //    {

        //    }



        //    return View();
        //}




        //[HttpPost]
        //public async Task<IActionResult> CreateCart(CartDto cartDto)
        //{
        //    var result = await _cartConsumeApi.CreateCart(cartDto);
        //    return View(result);
        //}
        //[HttpPost]
        //public async Task<IActionResult> UpdateCart(CartDto cartDto)
        //{
        //    var result = await _cartConsumeApi.UpdateCart(cartDto);
        //    return View(result);
        //}
        //[HttpPost]
        //public async Task<IActionResult> ClearCart(string userId)
        //{
        //    var result = await _cartConsumeApi.ClearCart(userId);
        //    return View(result);
        //}
        //[HttpPost]
        //public async Task<IActionResult> RemoveCart(int cartDetailsId)
        //{
        //    var result = await _cartConsumeApi.RemoveFromCart(cartDetailsId);
        //    return View(result);
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetCartById(string userId)
        //{
        //    var result = await _cartConsumeApi.GetCartByUserId(userId);
        //    return View(result);
        //}
    }
}
