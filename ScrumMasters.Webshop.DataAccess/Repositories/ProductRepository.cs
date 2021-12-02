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

        public PagedList<Product> GetProducts(ProductParameters productParameters)
        {
            return PagedList<Product>.ToPagedList(FindAll().OrderBy(product => product.ProductName),
                productParameters.PageNumber,
                productParameters.PageSize);
        }

        public Product FindById(int id)
        {
            return _context.Products.Select(product => new Product()
            {
                Id = product.Id,
                ProductName = product.ProductName
            }).FirstOrDefault(product => product.Id == id);
        }

        public Product Create(Product product)
        {
            var productEntity = new ProductEntity()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductImageUrl = product.ProductImageUrl
            };
            var savedEntity = _context.Products.Add(productEntity).Entity;
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

        public Product Update(Product product)
        {
            var productEntity = _context.Update(new ProductEntity
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductImageUrl = product.ProductImageUrl
            }).Entity;
            _context.SaveChanges();
            return new Product
            {
                Id = product.Id,
                ProductName = productEntity.ProductName,
                ProductPrice = productEntity.ProductPrice,
                ProductDescription = productEntity.ProductDescription,
                ProductImageUrl = productEntity.ProductImageUrl
            };
        }

        public Product DeleteById(int id)
        {
            var savedEntity = _context.Products.Remove(new ProductEntity() {Id = id}).Entity;
            _context.SaveChanges();
            return new Product()
            {
                Id = savedEntity.Id,
                ProductName = savedEntity.ProductName,
                ProductPrice = savedEntity.ProductPrice,
                ProductDescription = savedEntity.ProductDescription,
                ProductImageUrl = savedEntity.ProductImageUrl
            };
        }
    }
}