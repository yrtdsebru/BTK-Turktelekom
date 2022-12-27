using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Repositories.EFCore;
using Services.Contracts;

namespace ProductApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ProductController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult GetAllProducts([FromQuery] ProductRequestParameters p)
        {

            var products = _serviceManager.ProductService.GetAllProducts(p); 

            //ViewBag.Categories = categories;
            return View("GetAllProducts", products); //veri tabanımdaki productslarımı listeye çevirdim, product tablosunda ne varsa ekrana yaz

        }
        
        public IActionResult GetOneProduct(int id)
        {
            var product = _serviceManager.ProductService.GetOneProduct(id);

            return View("GetOneProduct",product);  //or return View(product);
        }

        public IActionResult GetAllProductsByCategoryId(int id)
        {

            var products = _serviceManager.ProductService.GetAllProductsByCategoryId(id);

            return View("GetAllProducts",products);  
        }
    

    }
}
