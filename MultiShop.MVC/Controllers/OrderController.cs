using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.EmailService;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderConsumeApi _order;
        private readonly IEmailSending _emailSending;
        public OrderController(IOrderConsumeApi order, IEmailSending emailSending)
        {
            _order = order;
            _emailSending = emailSending;
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
                await _emailSending.SendMessageAsync(order, "auxiliumnayatel");
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _order.GetOrderById(id);
            return View(result);
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
            var result = await _order.GetOrderById(id);
            return View(result);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _order.GetOrderById(id);
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _order.DeleteOrder(id);
            return RedirectToAction("index");
        }
    }
}
