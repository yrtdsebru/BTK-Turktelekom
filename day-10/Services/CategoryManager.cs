using Entities.Models;
using Repositories.Contract;
using Services.Contracts;


namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Category CreateOneCategory(Category category)
        {
            _manager.Category.Create(category);
            _manager.Save();
            return category;
        }

        public void DeleteOneCategory(int id)
        {

            //kategori varsa silsin yoksa hata fırlatsın. category'e logic ekleyecegiz. GetOneCa'a da yaptık bu islemi.
            var category = GetOneCategory(id);
            _manager.Category.Delete(category);
            _manager.Save();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _manager.Category.GetAllCategories();
        }

        public Category GetOneCategory(int id)
        {
            var category = _manager.Category.GetOneCategory(id);
            if (category == null)
                throw new Exception();
            return category;
        }

        public void Save()
        {
            _manager.Save();
        }

        public void UpdateOneCategory(Category category)
        {
            _manager.Category.Update(category);
            _manager.Save();
        }
    }
}
