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
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
           var data= await _userAccount.Login(login);
            if (data.Result.succeeded)
            {
                TempData["message"] = " <script> alert(' Login Successfully ') </script> ";
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _userAccount.LogOut();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetUserId()
        {
            await _userAccount.GetUserId(GetEmailAndUserId.Email);
            return RedirectToAction("Index", "Order");
        }
    }
}
