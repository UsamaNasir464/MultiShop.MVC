using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public async Task<IActionResult> CreateCart(CartDto cartDto)
        {
          var result =  await _cartConsumeApi.CreateCart(cartDto);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(CartDto cartDto)
        {
            var result = await _cartConsumeApi.UpdateCart(cartDto);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var result = await _cartConsumeApi.ClearCart(userId);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveCart(int cartDetailsId)
        {
            var result = await _cartConsumeApi.RemoveFromCart(cartDetailsId);
            return View(result);
        }
       [HttpGet]
       public async Task<IActionResult> GetCartById(string userId)
        {
          var result =   await _cartConsumeApi.GetCartByUserId(userId);
            return View(result);
        }
    }
}
