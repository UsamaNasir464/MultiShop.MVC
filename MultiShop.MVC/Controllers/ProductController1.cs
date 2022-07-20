using Microsoft.AspNetCore.Mvc;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.MVC.Controllers
{
    public class ProductController1 : Controller
    {
        private readonly IProducts _products;

        public ProductController1(IProducts products )
        {
            _products = products;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _products.GetAllProducts();
            return View();
        }
        [HttpGet]
       public IActionResult Create()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _products.CreateProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit( int id)
        {
            _products.GetProductsByID(id);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _products.EditProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _products.GetProductsByID(id);
            return View();
        }
        [HttpPost]
        public IActionResult DeleteConferm(int id)
        {
            _products.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            _products.GetProductsByID(id);
            return View();
        }
    }

}
