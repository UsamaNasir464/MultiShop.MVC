using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CartHeaderController : Controller
    {
        private readonly ICartHeaderConsumeApi _cartHeader;

        public CartHeaderController(ICartHeaderConsumeApi cartHeader)
        {
            _cartHeader = cartHeader;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
           var result = await  _cartHeader.GetAllCart();
            return View(result);
        }
     
        [HttpPost]
        public async Task<IActionResult> CreateCartHeader(CartCreateRequest cartCreateRequest)
        {
           var result =  await _cartHeader.CreateCart(cartCreateRequest);
            return View(result);
        }
        public async Task<IActionResult> EditCartHeader(CartHeader cartHeader)
        {
          var result = await _cartHeader.EditCart(cartHeader);
            return View(result);
        }
        public IActionResult DeleteCartHeader(int id)
        {
            var result = _cartHeader.DeleteCart(id);
            return View(result);
        }
        public async Task<IActionResult> GetCartById(int id)
        {
            var result = await _cartHeader.GetCartById(id);
            return View(result);
        }
    }
}
