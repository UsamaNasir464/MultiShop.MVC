using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Response;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class CartConsumeApi : ICartConsumeApi
    {
        private readonly HttpClient _httpClient;
        private readonly IProductConsumeApi _products;
        private readonly IDataProtector _dataProtector;
        protected readonly IConfiguration _config;
        public CartConsumeApi(HttpClient httpClient , IProductConsumeApi products, IDataProtectionProvider dataProtector, IConfiguration config)
        {
            _httpClient = httpClient;
            _products = products;
            _dataProtector = dataProtector.CreateProtector("So This Is MyData Protection Layer");
            _config = config;
        }
        public async Task<bool> ClearCart(string userId)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/CartApi/ClearCart/" + userId);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<CartDto> CreateCart(CartDto cartDto)
        {
            CartDto newCart = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<object>("api/CartApi/AddCart", cartDto);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCart = JsonConvert.DeserializeObject<CartDto>(result);
            }
            return newCart;
        }
        public async Task<CartDto> UpdateCart(CartDto cartDto)
        {
            CartDto newCart = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.PostAsJsonAsync<CartDto>("api/CartApi/UpdateCart/", cartDto);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                newCart = JsonConvert.DeserializeObject<CartDto>(result);
            }
            return newCart;
        }
        public async Task<CartDto> GetCartByUserId(string userId)
        {
            CartDto cartDto = null;
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("api/CartApi/GetCart/" + userId.ToString());
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                cartDto = JsonConvert.DeserializeObject<CartDto>(result);
            }
            return cartDto;
        }
        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            }
            var response = await _httpClient.GetAsync("/api/CartApi/RemoveCart/" + cartDetailsId.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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
            var baseAddress = _config.GetConnectionString("CurrentUrl");
            var cartDecryptUrl = _config.GetConnectionString("CartDecryptUrl");
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
            CartDto cartDto = new();
            if (response != null)
            {
                cartDto.CartDetails = response.CartDetails;
                cartDto.CartHeader = response.CartHeader;
            }
            return cartDto;
        }
    }
}
