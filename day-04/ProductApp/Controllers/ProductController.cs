using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {

        //constructer injection yapıyoruz, DI(Dependency Injection) 
        //Dependencie Injection(DI) 
        private readonly RepositoryContext _context; //DI'ları IoC uzerinden kontrol ediyoruz.
        //servise kayıt yaptık inversion of control(IoC), Sırtladığım frameworklerin akışının arasına giriyoruz. Biri sendden context isterse sen repository context gönder
        //IoC önemli adımlar =>kayıt(register), çözme(solve), Dispose(Lifecycle)[max cycle'ı yönetme].
        public ProductController(RepositoryContext context)
        {
            _context = context;
        }

        public IActionResult GetAllProducts()
        {
            var products = _context.Products.ToList();
            return View("GetAllProducts", products); //veri tabanımdaki productslarımı listeye çevirdim, product tablosunda ne varsa ekrana yaz
            //Index'e git giderken products da gotur
        }
        
        public IActionResult GetOneProduct(int id)
        {
            var product = _context.Products.Where(product => product.Id.Equals(id)).SingleOrDefault(); 

            return View("GetOneProduct",product);  //or return View(product);
        }


        //url'e yazınca otomatik GetAllProduct'a yonlendiriyor
        public IActionResult CreateOneProduct() {
            var product = new Product();
            //Id'yi otomatik alır
            product.ProductName = "Glass";
            product.Price = 2500;
            product.ImageUrl = "/images/products/glass.jpg";
            product.Description = "For sunny days.";
            product.AtCreated = DateTime.Now;

            _context.Products.Add(product); //repoya kaydediyoruz urunu
            _context.SaveChanges(); //kalıcı hale getiriyoruz.

            return RedirectToAction("GetAllProducts"); //Buradaki urun listesine gidecegiz RedirectToAction ile
        }

        //client->get->server

        [HttpGet]
        public IActionResult CreateOneProductWithView()
        {
            return View();
        }


        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneProductWithView(Product product)
        {
            /*            
            _context.Products.Add(product); //repoya kaydediyoruz urunu
            _context.SaveChanges(); //kalıcı hale getiriyoruz.

            return RedirectToAction("GetAllProduct");*/
            if(ModelState.IsValid)  //[Require] vs uyuyorsa 
            {
                _context.Add(product); //repoya kaydediyoruz urunu
                _context.SaveChanges(); //kalıcı hale getiriyoruz.

                return RedirectToAction("CreateOneProductWithView");
            }
            return View();
        }

        //veri geliyor
        [HttpGet] //yazamasak da olur default
        public IActionResult UpdateOneProduct([FromRoute(Name = "id")]int id)
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
                return RedirectToAction("GetAllProducts");
            }

            return View();
        }

        [HttpPost]  //silme islemi icin Post yeterli
        public IActionResult DeleteOneProduct(int id) {
            //1.Urunu veritabanindan sec
            //2.sil
            //3.degisiklikleri kaydet
            var product = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();

            /*         
            _context.Products.Remove(new Product() );
            _context.SaveChanges();
            
            ****parametreyi Product prd verirsek
            _context.Products.Remove(prd);
            _context.SaveChanges();*/

            return RedirectToAction("GetAllProducts");
        }
    }
}
