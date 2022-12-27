using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.EFCore;

namespace ProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        //****************DI Frame Start************
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public ProductController(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories,"CategoryId","CategoryName");   //Categorilere ulasabiliriz.
            return View(); 
        }

        //server->post->client
        [HttpPost]
        [ValidateAntiForgeryToken]   //bizim client'ımız degil de art niyetli baska bir client istekte bulunmasını engeller
        public IActionResult CreateOneProduct(ProductForInsertionDto productDto)
        {
            /*var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageUrl = productDto.ImageUrl
            };*/

            var product= _mapper.Map<Product>(productDto);  // product'tan Dto'ya gitmek istiyoruz.

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
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            var product = _context.Products.Where(x => x.Id == id).SingleOrDefault(); //Birdenn fazla kayit olabilir bana bir tanesini ver SingleorDefault
            //reverse mapping normalde View(product) yoluuyorduk şimdi View(ProductDto) göndermeliyiz. ya View(new Product
            //{
            //    Id = productDto.Id, 
            //    ProductName = productDto.ProductName,
            //    Price = productDto.Price,
            //    Description = productDto.Description,
            //    ImageUrl = productDto.ImageUrl
            //})
            

            //ya da git MappingPRofile'a Reverse ekle. CreateMap<ProductForUpdateDto, Product>().ReverseMap();

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
