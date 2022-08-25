using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.Mvc.Utills;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CartConsumeApi : ICartConsumeApi
    {
        private readonly IProductConsumeApi _products;
        private readonly IDataProtector _dataProtector;
        private readonly IApiCall apiCall;
        private readonly IConfiguration _config;

        public CartConsumeApi(IApiCall apiCall ,IProductConsumeApi products, IDataProtectionProvider dataProtector, IConfiguration config) 
        {
            this.apiCall = apiCall;
            _products = products;
            _config = config;
            _dataProtector = dataProtector.CreateProtector("So This Is MyData Protection Layer");
        }
        public async Task<bool> ClearCart(string userId)
        {
            return await apiCall.GetBoolAsync(_config.GetSection("ApiUrls:Cart:ClearCart").Value + userId);
        }
        public async Task<CartDto> CreateCart(CartDto cartDto)
        {
            return await apiCall.CallApiPostAsync<CartDto>(_config.GetSection("ApiUrls:Cart:CreateCart").Value, cartDto);
        }
        public async Task<CartDto> UpdateCart(CartDto cartDto)
        {
            return await apiCall.CallApiPostAsync<CartDto>(_config.GetSection("ApiUrls:Cart:UpdateCart").Value, cartDto);   
        }
        public async Task<CartDto> GetCartByUserId(string userId)
        {
            return await apiCall.CallApiGetAsync<CartDto>(_config.GetSection("ApiUrls:Cart:GetCartByUserId").Value + userId.ToString());   
        }
        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            return await apiCall.GetBoolAsync(_config.GetSection("ApiUrls:Cart:RemoveCart").Value + cartDetailsId.ToString());
        }
        public async Task<CartDto> LoginUserCart()
        {
            var userId = GetEmailAndUserId.UserId;
            var response = await GetCartByUserId(userId);
            CartDto cartDto = new();
            if (response != null)
            {
                cartDto.CartDetails = response.CartDetails;
                cartDto.CartHeader = response.CartHeader;
                if (cartDto?.CartHeader != null)
                {
                    foreach (var detail in cartDto.CartDetails)
                    {
                        var productId = detail.ProductFId;
                        var product = await _products.GetProductsById(productId);
                        detail.Product = product;
                        cartDto.CartHeader.OrderTotal += product.SalePrice * detail.Count;
                    }
                }
            }
            return cartDto;
        }
        public async Task<bool> AddToCart(int productId, int count)
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
            var productResponse = await _products.GetProductsById(productId);
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetailsDto);
            cartDetailsDto.Product = productResponse;
            cartDto.CartDetails = cartDetailsDtos;
            var cartCreate=await CreateCart(cartDto);
            if (cartCreate != null)
            {
                return true;
            }
            return false;
        }
        public string CreateCartUrl()
        {
            var userId = GetEmailAndUserId.UserId;
            string encryptedText = _dataProtector.Protect(userId);
            var baseAddress = _config.GetSection("ApiUrls:ConnectionUri:CurrentProjectUrl").Value;
            var cartDecryptUrl = _config.GetSection("ApiUrls:Cart:CartDecryptUrl").Value;
            var finalUrl = baseAddress + cartDecryptUrl + encryptedText;
            return finalUrl;
        }
        public async Task<bool> CartDecrypt(string urlToDecrypt)
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
                return true;
            }
            return false;
        }
        public async Task<bool> AddProductToUserCartUsingUrl(int productId, int count)
        {
            var cartDto = new CartDto()
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
            var productResponse = await _products.GetProductsById(productId);
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetailsDto);
            cartDetailsDto.Product = productResponse;
            cartDto.CartDetails = cartDetailsDtos;
            var addToCart = await CreateCart(cartDto);
            if (addToCart != null)
            {
                return true;
            }
            return false;
        }
        public async Task<CartDto> LoadCartDtoBasedOnRequestedUser(string decryptedUserId)
        {
            var response = await GetCartByUserId(decryptedUserId);
            var cartDto = new CartDto();
            if (response != null)
            {
                cartDto.CartDetails = response.CartDetails;
                cartDto.CartHeader = response.CartHeader;
            }
            return cartDto;
        }
    }
}
