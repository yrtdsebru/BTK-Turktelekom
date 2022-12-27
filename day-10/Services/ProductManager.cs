using AutoMapper;
using Entities.DataTransferObjects;
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

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public Product CreateOneProduct(ProductForInsertionDto productDto) //bu benim kaynagim DTO dan Entitiy'e döneceğiz
        {
            var product = _mapper.Map<Product>(productDto);
            _manager.Product.Create(product);
            _manager.Save();
            return product;
        }

        public void DeleteOneProduct(int id)
        {
            var product = GetOneProduct(id);
            _manager.Product.Delete(product);
            _manager.Save();

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

        public IEnumerable<Product> GetAllProductsWithDetail()
        {
            return _manager.Product.GetAllProductsWithDetail();
        }

        public Product GetOneProduct(int id)
        {
            var product = _manager.Product.GetOneProduct(id);
            if (product == null)
                throw new Exception();
            return product;
        }

        public ProductForUpdateDto GetOneProductForUpdate(int id)
        {
            var product = GetOneProduct(id);
            var productDto = _mapper.Map<ProductForUpdateDto>(product);
            return productDto;
        }

        public void UpdateOneProduct(ProductForUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);  // product'tan Dto'ya gitmek istiyoruz.
            _manager.Product.Update(product);
            _manager.Save();
        }
    }
}
