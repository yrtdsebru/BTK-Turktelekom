using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Services.Contracts;

namespace ProductApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CategoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            var Categories = _serviceManager.CategoryService.GetAllCategories();
            return View("Index", Categories);
        }

        public IActionResult GetOneCategory(int id)
        {
            var Category = _serviceManager.CategoryService.GetOneCategory(id);

            return View("GetOneCategory", Category);  
        }

        //client->get->server

        [HttpGet]
        public IActionResult CreateOneCategory()
        {
            return View();
        }


        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneCategory(Category Category)
        {
            if (Category is null)
                throw new Exception();

            if (ModelState.IsValid)  //[Require] vs uyuyorsa 
            {
                _serviceManager.CategoryService.CreateOneCategory(Category);

                return RedirectToAction("CreateOneCategory");
            }
            return View();
        }

        //veri geliyor
        [HttpGet] //yazamasak da olur default
        public IActionResult UpdateOneCategory([FromRoute(Name = "id")] int id)
        {
            //1.View olustur
            //2.İlgili urunu cekip View'e gondermek
            var Category = _serviceManager.CategoryService.GetOneCategory(id);//Birdenn fazla kayit olabilir bana bir tanesini ver SingleorDefault
            return View(Category);
        }

        //veritabanina yeni veriler gidicek ve veriyi güncelleyecek
        [HttpPost]       //.Net de []   filter/data attribute
        [ValidateAntiForgeryToken] //cross side effect'i engelliyor token kullanarak
        public IActionResult UpdateOneCategory(Category Category)
        {
            if (Category is null)
                throw new Exception();

            if (ModelState.IsValid)
            {
                _serviceManager.CategoryService.UpdateOneCategory(Category);  //Bu güncellese de biz goremeyiz degisiklik yapmiyo
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteOneCategory(int id)
        {
            _serviceManager.CategoryService.DeleteOneCategory(id);

            return RedirectToAction("Index");
        }
    }
}
