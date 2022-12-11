using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Contract;
using Repositories.EFCore;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        //****************DI Frame Start************
        //private readonly RepositoryContext _context;
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductController(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _manager.Product.GetAllProducts();  //modelimiz list of product 
            TempData["info"] = "Products have been listed.";  //bildirim ayarla
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateOneProduct() {  //get
            var categories = _manager.Category.GetAllCategories();
            ViewBag.Categories = new SelectList(categories,"CategoryId","CategoryName");   //Categorilere ulasabiliriz.
            return View(); 
        }

        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneProduct(ProductForInsertionDto productDto)
        {

            var product= _mapper.Map<Product>(productDto);  // product'tan Dto'ya gitmek istiyoruz.

            if (ModelState.IsValid)  //[Require] vs uyuyorsa 
            {
                _manager.Product.Create(product); //repoya kaydediyoruz urunu
                _manager.Save(); //kalıcı hale getiriyoruz.
                TempData["success"] = "Product has been created";
                return RedirectToAction("Index");
            }
            return View();
        }

        //veri geliyor
        [HttpGet] //yazamasak da olur default
        public IActionResult UpdateOneProduct([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = new SelectList(_manager.Category.GetAllCategories(), "CategoryId", "CategoryName");
            var product = _manager.Product.GetAllProductsByCategoryId(id);
            var productDto = _mapper.Map<ProductForUpdateDto>(product);
            return View(productDto);
        }

        //veritabanina yeni veriler gidicek ve veriyi güncelleyecek
        [HttpPost]       //.Net de []   filter/data attribute
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOneProduct(ProductForUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);  // product'tan Dto'ya gitmek istiyoruz.

            if (ModelState.IsValid)
            {
                product.AtCreated = DateTime.Now;
                //entity'nin izleme ozelligini kullanacagiz
                _manager.Product.Create(product);  //Bu güncellese de biz goremeyiz degisiklik yapmiyo
                _manager.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]  //silme islemi icin Post yeterli
        public IActionResult DeleteOneProduct(int id)
        {
            _manager.Product.Delete(new Product() { Id = id});
            _manager.Save();

            return RedirectToAction("Index");
        }
    }
}
