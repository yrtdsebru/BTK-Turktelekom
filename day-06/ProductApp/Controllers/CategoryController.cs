using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class CategoryController :Controller
    {
        private readonly RepositoryContext _context; //DI'ları IoC uzerinden kontrol ediyoruz. Veri tabanına gitmek icin connection ihtiyacı vs var onları kaldırıyor . Ios ile hallediyor
        public CategoryController(RepositoryContext context)
        {
            _context = context;
        }

        public IActionResult GetAllCategories()
        {
            var Categories = _context.Categories.ToList();
            return View("GetAllCategories", Categories); //veri tabanımdaki Categorieslarımı listeye çevirdim, Category tablosunda ne varsa ekrana yaz
            //Index'e git giderken Categories da gotur
        }

    }
}
