using Entities.Models;

namespace Repositories.Contract
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetAllCategories();
        Category GetOneCategory(int id);
    }
}
