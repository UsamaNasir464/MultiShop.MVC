using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.ServiceBus.Services;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class Products : BaseService , IProductConsumeApi
    {
        public Products(HttpClient httpClient,IConfiguration config): base(config , httpClient)
        {
        }
        public Product CreateProduct(ProductCreateRequest product)
        {
            Product newProduct = null;
            //Create Product Data Uploaded to Web Api included Image
            var file = product.ProductImage;
            byte[] data;
            string Name = product.Name;
            string Description = product.Description;
            string salePrice = product.SalePrice.ToString();
            string discountPrice = product.DiscountPrice.ToString();
            string catFId = product.CatFId.ToString();
            //MutiPartFormDataContent form sending information form form
            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(Name), "Name");
            multiForm.Add(new StringContent(Description), "Description");
            multiForm.Add(new StringContent(salePrice), "SalePrice");
            multiForm.Add(new StringContent(discountPrice), "DiscountPrice");
            multiForm.Add(new StringContent(catFId), "CatFId");
            //adding list of images in the MultipartFormDataContent with same key
            ByteArrayContent bytes;
            using (var br = new BinaryReader(file.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            }
            bytes = new ByteArrayContent(data);
            multiForm.Add(bytes, "ProductImage", product.ProductImage.FileName);
            CallBaseAddress();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("api/ProductApi/CreateProducts", multiForm).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                newProduct = JsonConvert.DeserializeObject<Product>(response);
            }
            return newProduct;
        }
        public bool DeleteProduct(int id)
        {
            CallBaseAddress();
            var response = _httpClient.DeleteAsync("api/ProductApi/DeleteProducts/" + id.ToString());
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<ProductEditRequest> EditProduct(ProductEditRequest product)
        {
            ProductEditRequest productsEdit = null;
            CallBaseAddress();
            var response = await _httpClient.PutAsJsonAsync<ProductEditRequest>("api/ProductApi/EditProducts", product);
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                productsEdit = JsonConvert.DeserializeObject<ProductEditRequest>(display);
            }
            return productsEdit;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/ProductApi/GetProductsList");
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(display);
            }
            return products;
        }
        public async Task<Product> GetProductsById(int id)
        {
            Product product = null;
            CallBaseAddress();
            var response = await _httpClient.GetAsync("api/ProductApi/GetProductsById/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(display);
            }
            return product;
        }
    }
}
