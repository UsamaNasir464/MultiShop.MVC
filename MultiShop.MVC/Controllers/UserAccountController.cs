using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{

    public class UserAccountController : Controller
    {
        private readonly IUserAccount _userAccount;

        public UserAccountController(IUserAccount userAccount)
        {
            _userAccount = userAccount;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            await _userAccount.CreateUserAsync(user);
            return RedirectToAction("Index", "Product");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            await _userAccount.Login(login);
            return RedirectToAction("Index", "Product");
        }
        public IActionResult LogOut()
        {
            _userAccount.LogOut();
            return RedirectToAction("Index", "Order");
        }
        public async Task<IActionResult> GetUserId()
        {
            var test = await _userAccount.GetUserId(GetEmailAndUserId.Email);
            return RedirectToAction("Index", "Order");
        }
    }
}
