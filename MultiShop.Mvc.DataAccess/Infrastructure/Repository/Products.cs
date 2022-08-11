using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
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
    public class Products : IProducts
    {
        private readonly HttpClient _httpClient;

        public Products(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Product> CreateProduct(ProductCreateRequest product)
        {
            Product newProduct = null;
            //Create Product Data Uploaded to Web Api included Image
            var file = product.ProductImage;
            byte[] data;
            string Name = product.Name;
            string Description = product.Description;

            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(Name), "Name");
            multiForm.Add(new StringContent(Description), "Description");
            multiForm.Add(new StringContent("1"), "CatFId");

            //adding list of images in the MultipartFormDataContent with same key
            ByteArrayContent bytes;
            using (var br = new BinaryReader(file.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            }
            bytes = new ByteArrayContent(data);
            multiForm.Add(bytes, "ProductImage", product.ProductImage.FileName);

            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
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
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
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
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
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
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
            var response = await _httpClient.GetAsync("api/ProductApi/GetProductsList");
            if (response.IsSuccessStatusCode)
            {
                var display = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<Product>>(display);

            }
            return products;
        }

        public async Task<Product> GetProductsByID(int id)
        {
            Product product = null;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/");
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
