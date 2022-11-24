using Microsoft.AspNetCore.Mvc;
using ProductAppNew.Models;
using System.Diagnostics;

namespace ProductAppNew.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            string msg = "Merhaba Models-Views-Controllers";
            return View("Index", msg);  //@model string ve <h1 class="display-4">@Model</h1>
            // return View("Index");
            // return View("Privacy");
        }

        public IActionResult Privacy()
        {

            return View();
        }

        public IActionResult GetProduct()
        {
            //ctrl+k+c yorum satırı
            //var product = new Product(); //instance
            //product.Id = 1;
            //product.ProductName = "Computer";
            //product.Price = 14000;

            //var product= new Product(1,"ebru",5000);

            var product = new Product() {
                Id = 2,
                ProductName = "computer",
                Price = 5000
            };

            return View(product);
            
            //return View(new Product()
            //{
            //    Id = 2,
            //    ProductName = "computer",
            //    Price = 5000
            //});
        }

        public IActionResult GetProducts()
        {
            var pList = new List<Product>()
            {
                new Product(1,"Computer",12000),
                new Product(2,"airpods",5000)
            };

            return View(pList);
        }

    }
}