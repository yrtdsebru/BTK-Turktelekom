using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;

namespace ProductApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        string msg = "selam";
        return View("Index",msg);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    /*
    //create an action and url ekle view'e ekleyerek
    public IActionResult GetOneProduct() { 
        
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

        var pList = new List<Product>();
        pList.Add(new Product(1,"Computer",12000));
        pList.Add(new Product(2,"Airpods",5000));
        pList.Add(new Product(3,"Laptop",15000));

        return View(pList);
    } */

}
