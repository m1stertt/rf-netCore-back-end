using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        public PagedCategoriesProductList<Product> GetPagedCategoryProducts(CategoriesPaginationParameters productParameters);
        Category DeleteById(int id);
        Category Update(Category category);
        Category Create(Category category);
    }
}