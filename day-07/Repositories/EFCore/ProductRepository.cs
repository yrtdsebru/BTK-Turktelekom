using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contract;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository  //Bu base'i kalıt repository için
    {
        //private readonly RepositoryContext _context;

        //public ProductRepository(RepositoryContext context)
        //{
        //    _context = context;
        //}

        public ProductRepository(RepositoryContext context) : base(context)
        {
        }


        public IEnumerable<Product> GetAllProducts() =>
                FindAll();            //_context.Products.ToList();

        public IEnumerable<Product> GetAllProducts(ProductRequestParameters p)
        {
            return _context.Products
                .FilterProducts(p.MinPrice, p.MaxPrice)
                .Search(p.SearchTerm)
                .ToList();
        }

        public IEnumerable<Product> GetAllProductsByCategoryId(int categoryId)
        {
            return _context.Products
                .Where(p => p.CategoryId.Equals(categoryId))
                .ToList();
        }

        public Product GetOneProduct(int id)
        {
            return _context.Products.Where(p => p.Id.Equals(id)).SingleOrDefault();
        }
    }
}
