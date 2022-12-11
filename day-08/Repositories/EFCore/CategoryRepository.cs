using Entities.Models;
using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();  //Bunu artık .FindAll(); ile de tamamlayabiliriz. Base sayesinde
        }

        public Category GetOneCategory(int id) =>
            FindById(c => c.CategoryId.Equals(id)); //_context.Categories.Where(c => c.CategoryId.Equals(id)).FirstOrDefault(); 
        
    }
}
