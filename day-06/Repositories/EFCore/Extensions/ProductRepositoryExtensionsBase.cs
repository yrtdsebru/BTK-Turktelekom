using Entities.Models;

namespace Repositories.EFCore.Extensions
{
    public static class ProductRepositoryExtensionsBase
    {
        public static IQueryable<Product>
            FilterProducts(this IQueryable<Product> products, uint minPrice, uint maxPrice)
        {
            return products.Where(prd => prd.Price >= minPrice);
        }
    }
}