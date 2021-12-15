using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IProductService
    {
        PagedProductList<Product> GetPagedProductList(ProductPaginationParameters productParameters);

        PagedCategoriesProductList<Product> GetPagedCategoryProducts(
            CategoriesPaginationParameters categoriesPaginationParameters);
        Product GetProductById(int id);
        Product Create(Product product);
       Product Update(Product product);
       Product DeleteById(int id);
       List<Product> GetFeaturedProducts();
    }
}