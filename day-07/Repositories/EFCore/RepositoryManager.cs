using Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _productRepository = new ProductRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }

        //Bu sekilde de olur. Yukarıdaki daha iyi.
        //public RepositoryManager(RepositoryContext context, IProductRepository productRepository, ICategoryRepository categoryRepository)
        //{
        //    _context = context;
        //    _productRepository = productRepository;
        //    _categoryRepository = categoryRepository;
        //}

        public IProductRepository Product => _productRepository;

        public ICategoryRepository Category => _categoryRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
