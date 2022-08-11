using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.Request;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProducts _products;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProducts products, IWebHostEnvironment hostEnvironment)
        {
            _products = products;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _products.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest product)
        {
             await _products.CreateProduct(product);

            var file = product.ProductImage;
            byte[] data;
            string name = product.Name;
            string description = product.Description;
            string salePrice = product.SalePrice.ToString();
            string discountPrice = product.DiscountPrice.ToString();
            string catFId = product.CatFId.ToString();

            //MultiPart For Uploading Content/Image
            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(name), "Name");
            multiForm.Add(new StringContent(description), "Description");
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

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44398/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.PostAsync("api/ProductApi/CreateProducts", multiForm).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _products.GetProductsByID(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditRequest product)
        {
            await _products.EditProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _products.GetProductsByID(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            _products.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _products.GetProductsByID(id);
            return View(result);
        }
    }

}
