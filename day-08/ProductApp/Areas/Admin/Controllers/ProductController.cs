using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Contract;
using Repositories.EFCore;
using Services.Contracts;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        //****************DI Frame Start************
        //private readonly RepositoryContext _context;
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var products = _manager.ProductService.GetAllProductsWithDetail();  //modelimiz list of product 
            TempData["info"] = "Products have been listed.";  //bildirim ayarla
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateOneProduct() {  //get
            var categories = _manager.CategoryService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories,"CategoryId","CategoryName");   //Categorilere ulasabiliriz.
            return View(); 
        }

        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneProduct(ProductForInsertionDto productDto)
        {
            if (ModelState.IsValid)  //[Require] vs uyuyorsa 
            {
                _manager.ProductService.CreateOneProduct(productDto); //repoya kaydediyoruz urunu
                TempData["success"] = "Product has been created";
                return RedirectToAction("Index");
            }
            return View();
        }

        //veri geliyor
        [HttpGet] //yazamasak da olur default
        public IActionResult UpdateOneProduct([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = new SelectList(_manager.CategoryService.GetAllCategories(), "CategoryId", "CategoryName");
            var productDto = _manager.ProductService.GetOneProductForUpdate(id);    
            return View(productDto);
        }

        //veritabanina yeni veriler gidicek ve veriyi güncelleyecek
        [HttpPost]       //.Net de []   filter/data attribute
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOneProduct(ProductForUpdateDto productDto)
        {

            if (ModelState.IsValid)
            {
                _manager.ProductService.UpdateOneProduct(productDto);  //Bu güncellese de biz goremeyiz degisiklik yapmiyo
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]  //silme islemi icin Post yeterli
        public IActionResult DeleteOneProduct(int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            return RedirectToAction("Index");
        }
    }
}
