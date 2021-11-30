using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product Create(Product product);
    }
}