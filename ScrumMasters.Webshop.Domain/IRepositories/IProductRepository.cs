using System.Collections.Generic;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> FindAll();
        PagedProductList<Product> GetPagedProductList(ProductPaginationParameters productParameters);
        PagedCategoriesProductList<Product> GetPagedCategoriesProductList(
            CategoriesPaginationParameters categoriesPaginationParameters);
        Product FindById(int id);
        Product Create(Product product);
        Product Update(Product product);
        Product DeleteById(int id);
        List<Product> GetFeaturedProducts();
    }
}