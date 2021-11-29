using System.Collections.Generic;
using ScrumMasters.Webshop.Core.Models;

namespace ScrumMasters.Webshop.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> FindAll();
    }
}