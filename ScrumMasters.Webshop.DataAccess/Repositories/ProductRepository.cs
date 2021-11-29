using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        
        private readonly MainDbContext context;
        
        public ProductRepository(MainDbContext ctx)
        {
            context = ctx ?? throw new InvalidDataException("Product Repository Must have a DBContext");
        }
        public List<Product> FindAll()
        {
            return context.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    ProductName = pe.ProductName,
                    ProductPrice = pe.ProductPrice,
                    ProductDescription = pe.ProductDescription,
                    ProductImageUrl = pe.ProductImageUrl
                })
                .ToList();
        }
    }
}