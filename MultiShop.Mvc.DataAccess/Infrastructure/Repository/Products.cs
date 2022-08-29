using Microsoft.Extensions.Configuration;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using MultiShop.Mvc.Models.ViewModels;
using MultiShop.Mvc.Utills;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiShop.Mvc.DataAccess.Infrastructure.Repository
{
    public class Products : IProductConsumeApi
    {
        private readonly IApiCall apiCall;
        private readonly IConfiguration config;

        public Products(IApiCall apiCall, IConfiguration config)
        {
            this.apiCall = apiCall;
            this.config = config;
        }
        public Product CreateProduct(ProductCreateRequest product)
        {
            Product newProduct = null;
            //Create Product Data Uploaded to Web Api included Image
            var file = product.ProductImage;
            byte[] data;
            //string Name = product.Name;
            //string Description = product.Description;
            //string salePrice = product.SalePrice.ToString();
            //string discountPrice = product.DiscountPrice.ToString();
            //string catFId = product.CatFId.ToString();
            //MutiPartFormDataContent form sending information form form
            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(product.Name), "Name");
            multiForm.Add(new StringContent(product.Description), "Description");
            multiForm.Add(new StringContent(product.SalePrice.ToString()), "SalePrice");
            multiForm.Add(new StringContent(product.DiscountPrice.ToString()), "DiscountPrice");
            multiForm.Add(new StringContent(product.CatFId.ToString()), "CatFId");
            //adding list of images in the MultipartFormDataContent with same key
            ByteArrayContent bytes;
            using (var br = new BinaryReader(file.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            }
            bytes = new ByteArrayContent(data);
            multiForm.Add(bytes, "ProductImage", product.ProductImage.FileName);
            apiCall.CallApiPostAsync<Product, MultipartFormDataContent>(config.GetSection("ApiUrls:Products:CreateProduct").Value, multiForm);
            return newProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await apiCall.CallApiDeleteAsync(config.GetSection("ApiUrls:Products:DeleteProduct").Value + id.ToString());
        }

        public async Task<ProductEditRequest> EditProduct(ProductEditRequest product)
        {
            return await apiCall.CallApiPostAsync<ProductEditRequest>(config.GetSection("ApiUrls:Products:EditProduct").Value, product);
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await apiCall.CallApiGetAsync<List<Product>>(config.GetSection("ApiUrls:Products:GetAllProducts").Value);
        }
        public async Task<Product> GetProductsById(int id)
        {
            return await apiCall.CallApiGetAsync<Product>(config.GetSection("ApiUrls:Products:GetProductById").Value + id.ToString());
        }
    }
}
