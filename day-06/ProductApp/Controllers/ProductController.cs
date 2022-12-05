using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult GetAllProducts([FromQuery] ProductRequestParameters p)
        {
            var products = _productRepository.GetAllProducts(p);
            return View("GetAllProducts", products); //veri tabanımdaki productslarımı listeye çevirdim, product tablosunda ne varsa ekrana yaz
            //Index'e git giderken products da gotur
        }
        
        public IActionResult GetOneProduct(int id)
        {
            var product = _productRepository.GetOneProduct(id);

            return View("GetOneProduct",product);  //or return View(product);
        }
    

    }
}
