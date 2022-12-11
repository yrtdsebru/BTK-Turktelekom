using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contract
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetAllProducts();   //metot imzası, metot signature
        Product GetOneProduct(int id);
        IEnumerable<Product> GetAllProducts(ProductRequestParameters p);
        IEnumerable<Product> GetAllProductsByCategoryId(int id);  

    }
}
