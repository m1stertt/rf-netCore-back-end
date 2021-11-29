using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
    }
}