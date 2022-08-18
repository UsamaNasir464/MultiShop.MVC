using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class QueryController : Controller
    {
        private readonly IDataProtector _dataProtector;
        protected readonly IConfiguration _config;
        private readonly IProducts _products;
        private readonly ICartConsumeApi _cartConsumeApi;
        public QueryController(IDataProtectionProvider dataProtector, IConfiguration config, IProducts products
            , ICartConsumeApi cartConsumeApi)
        {
            _dataProtector = dataProtector.CreateProtector("So This Is MyData Protection Layer");
            _config = config;
            _products = products;
            _cartConsumeApi = cartConsumeApi;
        }
        public IActionResult Index()
        {
            var userId = GetEmailAndUserId.UserId;
            string encryptedText = _dataProtector.Protect(userId);
            var baseAddress = _config.GetConnectionString("CurrentUrl");
            var cartDeserializerUrl = "Query/CartDecrypt/";
            var finalUrl = baseAddress + cartDeserializerUrl + encryptedText;
            return RedirectToAction("CopyCart");
        }

        public IActionResult CopyCart()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CartDecrypt(string urlToDecrypt)
        {
            string[] urlToSplit = urlToDecrypt.Split("/");
            string decryptedUserId = _dataProtector.Unprotect(urlToSplit[5]);

            CartDto cartDto = await LoadCartDtoBasedOnRequestedUser(decryptedUserId);
            if (cartDto.CartDetails != null)
            {
                foreach (var details in cartDto.CartDetails)
                {
                    await AddProductToUserCartUsingUrl(details.ProductFId, details.Count);
                }
            }

            return RedirectToAction("CartIndex", "Cart");

        }
        private async Task<bool> AddProductToUserCartUsingUrl(int productId, int count)
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
                ProductFId = productId
            };
            var productResponse = await _products.GetProductsByID(productId);
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetailsDto);
            cartDetailsDto.Product = productResponse;
            cartDto.CartDetails = cartDetailsDtos;

            var addToCart = await _cartConsumeApi.CreateCart(cartDto);
            if (addToCart != null)
            {
                return true;
            }
            return false;
        }



        private async Task<CartDto> LoadCartDtoBasedOnRequestedUser(string decryptedUserId)
        {
            var response = await _cartConsumeApi.GetCartByUserId(decryptedUserId);

            CartDto cartDto = new();


            if (response != null)
            {
                cartDto.CartDetails = response.CartDetails;
                cartDto.CartHeader = response.CartHeader;

                if (cartDto?.CartHeader != null)
                {
                    foreach (var detail in cartDto.CartDetails)
                    {
                        var product = await _products.GetProductsByID(detail.ProductFId);
                        detail.Product = product;
                        cartDto.CartHeader.OrderTotal += product.SalePrice * detail.Count;
                    }
                }
            }
            return cartDto;
        }

    }
}
