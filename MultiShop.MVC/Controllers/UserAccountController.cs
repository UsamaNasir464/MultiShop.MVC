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
            var userId = await _userAccount.GetUserId(GetEmailAndUserId.Email);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var loggedin=await _userAccount.Login(login);
            var test = await _userAccount.GetUserId(GetEmailAndUserId.Email);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            _userAccount.LogOut();
            GetEmailAndUserId.UserId = null;
            GetEmailAndUserId.Email = null;
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetUserId()
        {
            var test = await _userAccount.GetUserId(GetEmailAndUserId.Email);
            return RedirectToAction("Index", "Order");
        }
    }
}
