using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
                    ProductImageUrl = pe.ProductImageUrl,
                    //Categories = pe.Categories
                })
                .ToList();
        }

        public Product FindById(int id)
        {
            return _context.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                ProductName = pe.ProductName,
                ProductPrice = pe.ProductPrice,
                ProductDescription = pe.ProductDescription,
                ProductImageUrl = pe.ProductImageUrl,
                Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList()
            }).FirstOrDefault(product => product.Id == id);
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

        public Product Update(Product product)
        {
            ProductEntity edit= _context.Products.Include("Categories").Single(productt => productt.Id == product.Id);
            edit.Categories.Clear();
            edit.ProductName = product.ProductName;
            edit.ProductPrice = product.ProductPrice;
            edit.ProductDescription = product.ProductDescription;
            edit.ProductImageUrl = product.ProductImageUrl;
            edit.Categories = product.Categories.Select(px =>_context.Categories.Single(pe=>px.Id==pe.Id)).ToList();
            _context.SaveChanges();
            return product;
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