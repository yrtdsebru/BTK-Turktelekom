using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Repositories.EFCore;

namespace ProductApp.Controllers
{
    public class CategoryController :Controller
    {
        //private readonly RepositoryContext _context; //DI'ları IoC uzerinden kontrol ediyoruz. Veri tabanına gitmek icin connection ihtiyacı vs var onları kaldırıyor . Ios ile hallediyor
        //public CategoryController(RepositoryContext context)
        //{
        //    _context = context;
        //}

        private readonly IRepositoryManager _manager;

        public CategoryController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IActionResult GetAllCategories()
        {
            var Categories = _manager.Category.GetAllCategories();
            return View(Categories); //veri tabanımdaki Categorieslarımı listeye çevirdim, Category tablosunda ne varsa ekrana yaz
            //Index'e git giderken Categories da gotur
        }

    }
}
