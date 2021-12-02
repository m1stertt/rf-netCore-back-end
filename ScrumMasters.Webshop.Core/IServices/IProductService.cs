using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Core.IServices
{
    public interface IProductService
    {
        PagedList<Product> GetProducts(ProductParameters productParameters);
        Product GetProductById(int id);
        Product Create(Product product);
       Product Update(Product product);
       Product DeleteById(int id);

    }
}