using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProductsWithDetail();
        IEnumerable<Product> GetAllProducts();   //metot imzası, metot signature
        Product GetOneProduct(int id);
        IEnumerable<Product> GetAllProducts(ProductRequestParameters p);
        IEnumerable<Product> GetAllProductsByCategoryId(int id);
        Product CreateOneProduct(ProductForInsertionDto productDto); //imza olusturduk productmanager icin. Bana bir product ver içinde de bir DTO ver.
        ProductForUpdateDto GetOneProductForUpdate(int id);   //get icin
        void UpdateOneProduct(ProductForUpdateDto productDto);  //post icin
        void DeleteOneProduct(int id);
    }
}
