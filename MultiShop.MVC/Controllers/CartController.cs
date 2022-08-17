using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.EmailService;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartConsumeApi _cartConsumeApi;
        private readonly IProducts _productConsumeApi;
        private readonly IOrderConsumeApi _orderConsumeApi;
        private readonly IOrderDetailsConsuumeApi _orderDetail;
        private readonly IEmailSending _emailSending;

        public CartController(ICartConsumeApi cartConsumeApi, IProducts productConsumeApi, IOrderConsumeApi orderConsumeApi
            , IOrderDetailsConsuumeApi orderDetail, IEmailSending emailSending)
        {
            _cartConsumeApi = cartConsumeApi;
            _productConsumeApi = productConsumeApi;
            _orderConsumeApi = orderConsumeApi;
            _orderDetail = orderDetail;
            _emailSending = emailSending;
        }

        public async Task<IActionResult> CartIndex()
        {
            
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Checkout()
        {
            OrderAndCartDto orderAndCart = new();
            var response = await LoadCartDtoBasedOnLoggedInUser();
           


            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Checkout(CartDto orderCart)
        {
            var userId = GetEmailAndUserId.UserId;
            var response = await _cartConsumeApi.GetCartByUserId(userId);
            CartDto cartDto = new();
            cartDto.CartDetails = response.CartDetails;
            cartDto.CartHeader = response.CartHeader;
            foreach (var detail in cartDto.CartDetails)
            {
                var product = await _productConsumeApi.GetProductsByID(detail.ProductFId);
                detail.Product = product;
                cartDto.CartHeader.OrderTotal += product.SalePrice * detail.Count;
            }
            OrderCreateRequest order = new();
            order.PaymentMethod = "Cod";
            order.OrderDate = System.DateTime.Now;
            order.OrderType = "Sale";
            order.GrandTotal = (cartDto.CartHeader.OrderTotal + (cartDto.CartHeader.OrderTotal/100));
            order.Address = orderCart.Order.Address;
            order.CustomerName = orderCart.Order.CustomerName;
            order.Email = orderCart.Order.Email;
            order.PhoneNumber = orderCart.Order.PhoneNumber;
            order.UserFid = userId;
            
            var test=await _orderConsumeApi.CreateOrder(order);
            OrderDetailsCreateRequest orderdetail = new();
            foreach (var item in cartDto.CartDetails)
            {
                orderdetail.ProductFId = item.ProductFId;
                orderdetail.OrderFId = test.Order.Id;
                orderdetail.ProductQuantity = item.Count;
                orderdetail.SalePrice = item.Product.SalePrice;
                orderdetail.TotalPrice = item.Product.SalePrice * item.Count;
                var result=await _orderDetail.CreateOrderDetails(orderdetail);
            }
            await _cartConsumeApi.ClearCart(userId);
           await _emailSending.SendMessageAsync(order, "auxiliumnayatel");
            return RedirectToAction("index","Home");
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
