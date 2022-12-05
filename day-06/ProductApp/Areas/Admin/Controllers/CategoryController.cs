using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly RepositoryContext _context; //DI'ları IoC uzerinden kontrol ediyoruz. Veri tabanına gitmek icin connection ihtiyacı vs var onları kaldırıyor . Ios ile hallediyor
        public CategoryController(RepositoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Categories = _context.Categories.ToList();
            return View("Index", Categories); //veri tabanımdaki Categorieslarımı listeye çevirdim, Category tablosunda ne varsa ekrana yaz
            //Index'e git giderken Categories da gotur
        }

        public IActionResult GetOneCategory(int id)
        {
            var Category = _context.Categories.Where(x => x.CategoryId.Equals(id)).SingleOrDefault();

            return View("GetOneCategory", Category);  //or return View(Category);
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
                _context.Categories.Add(Category); //repoya kaydediyoruz urunu
                _context.SaveChanges(); //kalıcı hale getiriyoruz.

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
            var Category = _context.Categories.Where(x => x.CategoryId.Equals(id)).FirstOrDefault(); //Birdenn fazla kayit olabilir bana bir tanesini ver SingleorDefault
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
                _context.Categories.Update(Category);  //Bu güncellese de biz goremeyiz degisiklik yapmiyo
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult DeleteOneCategory(int id)
        {
            var Category = _context.Categories.Where(x => x.CategoryId == id).SingleOrDefault();
            _context.Categories.Remove(Category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
