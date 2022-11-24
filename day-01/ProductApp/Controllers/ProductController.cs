using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using System.Diagnostics;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetOneProduct()
        {

            //1.veriyi organize etme
            Product prd = new Product();
            prd.Id = 1;
            prd.ProductName = "laptop";
            prd.Price = 500;

            //2. view'a gönder
            return View(prd);   //gönderdiğimiz veri türü string değilse direkt içine yaz string olsayı View("GetOneProduct", prd); seklinde yazariz
        }

        public IActionResult GetAllProducts()
        {
            /*var pList = new List<Product>()
                {
                    new Product(1,"Computer",12000),
                    new Product(2,"AirPods",5000),
                    new Product(3,"Laptop",15000)
                };
            */

            var pList = new List<Product>();
            pList.Add(new Product(1, "Computer", 12000));
            pList.Add(new Product(2, "Airpods", 5000));
            pList.Add(new Product(3, "Laptop", 15000));

            return View(pList);
        }
    }
}
