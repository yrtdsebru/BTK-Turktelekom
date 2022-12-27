using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contract;
using Services.Contracts;

namespace ProductApp.Controllers
{
    public class CategoryController :Controller
    {
        private readonly IServiceManager _serviceManager;

        public CategoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult GetAllCategories()
        {
            var categories = _serviceManager.CategoryService.GetAllCategories();
            return View(categories);
        }

    }
}
