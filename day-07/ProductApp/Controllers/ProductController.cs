using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryManager _manager;

        public ProductController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult GetAllProducts([FromQuery] ProductRequestParameters p)
        {
            //Categorileri al ve View'e gonder.
            //var categories = _categoryRepository.GetAllCategories();
            
            var products = _manager.Product.GetAllProducts(p);

            //ViewBag.Categories = categories;
            return View("GetAllProducts", products); //veri tabanımdaki productslarımı listeye çevirdim, product tablosunda ne varsa ekrana yaz
            //Index'e git giderken products da gotur
        }
        
        public IActionResult GetOneProduct(int id)
        {
            var product = _manager.Product.GetOneProduct(id);

            return View("GetOneProduct",product);  //or return View(product);
        }

        public IActionResult GetAllProductsByCategoryId(int id)
        {
            //var categories = _categoryRepository.GetAllCategories();
            //ViewBag.Categories = categories;

            var products = _manager.Product
                .GetAllProductsByCategoryId(id);

            return View("GetAllProducts",products);  
        }
    

    }
}
