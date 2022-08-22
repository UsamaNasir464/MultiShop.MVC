using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderConsumeApi _order;
        public OrderController(IOrderConsumeApi order)
        {
            _order = order;
        }
        public async Task<ActionResult> Index()
        {
            List<Order> allOrders = await _order.GetAllOrders();
            return View(allOrders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateRequest order)
        {
            if (ModelState.IsValid)
            {
                await _order.CreateOrder(order);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _order.GetOrderById(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(OrderEditRequest order)
        {
            if (ModelState.IsValid)
            {
                await _order.EditOrder(order);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Details(int id)
        {
            return View(await _order.GetOrderById(id));
        }
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _order.GetOrderById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _order.DeleteOrder(id);
            return RedirectToAction("index");
        }
        public async Task<IActionResult> OrderConfirmed(CartDto orderCart)
        {
            bool confirmed= await _order.OrderConfirmed(orderCart);
            if (confirmed)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Checkout", "Cart");
        }
    }
}