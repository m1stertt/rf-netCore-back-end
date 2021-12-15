using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        List<Category> FindAll();
        PagedCategoriesProductList<Product> GetPagedCategoriesProductList(CategoriesPaginationParameters categoriesPaginationParameters);
        Category GetById(int id);
        Category DeleteById(int id);
        Category Update(Category category);
        Category Create(Category category);
    }
}