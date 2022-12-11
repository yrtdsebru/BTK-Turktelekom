using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contract;
using Services.Contracts;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _manager.Product.GetAllProducts();
        }

        public IEnumerable<Product> GetAllProducts(ProductRequestParameters p)
        {
            return _manager.Product.GetAllProducts(p);
        }

        public IEnumerable<Product> GetAllProductsByCategoryId(int id)
        {
            return _manager.Product.GetAllProductsByCategoryId(id);
        }

        public Product GetOneProduct(int id)
        {
            var product = _manager.Product.GetOneProduct(id);
            if (product == null)
                throw new Exception();
            return product;
        }
    }
}
