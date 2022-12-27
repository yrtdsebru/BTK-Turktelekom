using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class ProductController1 : Controller
    {
        private readonly RepositoryContext _context;


        //constructer injection yapıyoruz, DI(Dependency Injection), IoC bak bunlara önemli 
        public ProductController1(RepositoryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.ToList());  //veri tabanımdaki productslarımı listeye çevirdim
        }
    }
}
