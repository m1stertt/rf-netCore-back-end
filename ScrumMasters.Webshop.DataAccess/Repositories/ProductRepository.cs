using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        
        private readonly MainDbContext _context;
        
        public ProductRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Product Repository Must have a DBContext");
        }
        public List<Product> FindAll()
        {
            return _context.Products
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

        public Product Create(Product product)
        {
            var entity = new ProductEntity()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductImageUrl = product.ProductImageUrl
            };
            var savedEntity = _context.Products.Add(entity).Entity;
            _context.SaveChanges();
            return new Product()
            {
                Id = product.Id,
                ProductName = savedEntity.ProductName,
                ProductPrice = savedEntity.ProductPrice,
                ProductDescription = savedEntity.ProductDescription,
                ProductImageUrl = savedEntity.ProductImageUrl
            };
        }
    }
}