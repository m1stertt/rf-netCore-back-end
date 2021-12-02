using System.Collections.Generic;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> FindAll();
        PagedList<Product> GetProducts(ProductParameters productParameters);
        Product FindById(int id);
        Product Create(Product product);
        Product Update(Product product);
        Product DeleteById(int id);
    }
}