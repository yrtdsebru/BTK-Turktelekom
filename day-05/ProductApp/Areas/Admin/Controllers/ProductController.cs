using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        //****************DI Frame Start************
        private readonly RepositoryContext _context;

        public ProductController(RepositoryContext context)
        {
            _context = context;
        }

        //******************DI End********************
        //Bunu kullanabilmek icin IOS'de inversional context
        public IActionResult Index()
        {
            var products = _context.Products.ToList();  //modelimiz list of product 
            TempData["info"] = "Products have been listed.";  //bildirim ayarla
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateOneProduct() {  //get
            
            return View(); 
        }

        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneProduct(Product product)
        {
            if (ModelState.IsValid)  //[Require] vs uyuyorsa 
            {
                _context.Add(product); //repoya kaydediyoruz urunu
                _context.SaveChanges(); //kalıcı hale getiriyoruz.
                TempData["success"] = "Product has been created";
                return RedirectToAction("Index");
            }
            return View();
        }

        //veri geliyor
        [HttpGet] //yazamasak da olur default
        public IActionResult UpdateOneProduct([FromRoute(Name = "id")] int id)
        {
            //1.View olustur
            //2.İlgili urunu cekip View'e gondermek
            var product = _context.Products.Where(x => x.Id == id).SingleOrDefault(); //Birdenn fazla kayit olabilir bana bir tanesini ver SingleorDefault
            return View(product);
        }

        //veritabanina yeni veriler gidicek ve veriyi güncelleyecek
        [HttpPost]       //.Net de []   filter/data attribute
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOneProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.AtCreated = DateTime.Now;
                //entity'nin izleme ozelligini kullanacagiz
                _context.Products.Update(product);  //Bu güncellese de biz goremeyiz degisiklik yapmiyo
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]  //silme islemi icin Post yeterli
        public IActionResult DeleteOneProduct(int id)
        {
            //1.Urunu veritabanindan sec
            //2.sil
            //3.degisiklikleri kaydet
            var product = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
