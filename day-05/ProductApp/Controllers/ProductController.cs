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
    
    }
}
