using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Domain.IRepositories;
using Size = ScrumMasters.Webshop.Core.Models.Size;
using Color = ScrumMasters.Webshop.Core.Models.Color;

namespace ScrumMasters.Webshop.DataAccess.Repositories
{//https://github.com/dotnet/efcore/issues/22868
    public class ProductRepository : IProductRepository
    {
        private readonly MainDbContext _context;

        public ProductRepository(MainDbContext ctx)
        {
            _context = ctx ?? throw new InvalidDataException("Product Repository Must have a DBContext");
        }

        public List<Product> FindAll()
        {
            return _context.Products.Select(pe => new Product
                {
                    Id = pe.Id,
                    ProductName = pe.ProductName,
                    ProductPrice = pe.ProductPrice,
                    ProductDescription = pe.ProductDescription,
                    ProductImageUrl = pe.ProductImageUrl,
                    ProductFeatured = pe.ProductFeatured,
                    Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                    Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                    Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title}).ToList(),
                    Images = pe.Images.Select(px=>new Image{Id = px.Id,Title = px.Title,Path=px.Path}).ToList(),
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
            return _context.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                ProductName = pe.ProductName,
                ProductPrice = pe.ProductPrice,
                ProductDescription = pe.ProductDescription,
                ProductImageUrl = pe.ProductImageUrl,
                ProductFeatured = pe.ProductFeatured,
                Categories = pe.Categories.Select(px=>new Category{Id = px.Id,Name = px.Name}).ToList(),
                Sizes = pe.Sizes.Select(px=>new Size{Id = px.Id,Title = px.Title}).ToList(),
                Colors = pe.Colors.Select(px=>new Color{Id = px.Id,Title = px.Title}).ToList(),
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
                ProductImageUrl = product.ProductImageUrl,
                //ProductFeatured = product.ProductFeatured,
            };
            var savedEntity = _context.Products.Add(productEntity).Entity;
            _context.SaveChanges();
            return new Product()
            {
                Id = product.Id,
                ProductName = savedEntity.ProductName,
                ProductPrice = savedEntity.ProductPrice,
                ProductDescription = savedEntity.ProductDescription,
                ProductImageUrl = savedEntity.ProductImageUrl,
                //ProductFeatured = savedEntity.ProductFeatured,
            };
        }

        public Product Update(Product product)
        {
            ProductEntity edit= _context.Products.Include("Categories").Include("Colors").Include("Sizes").Single(productt => productt.Id == product.Id);
            edit.Categories.Clear();
            edit.Colors.Clear();
            edit.Sizes.Clear();
            edit.ProductName = product.ProductName;
            edit.ProductPrice = product.ProductPrice;
            edit.ProductDescription = product.ProductDescription;
            edit.ProductImageUrl = product.ProductImageUrl;
            edit.ProductFeatured = product.ProductFeatured;
            edit.Categories = product.Categories.Select(px =>_context.Categories.Single(pe=>px.Id==pe.Id)).ToList();
            edit.Sizes = product.Sizes.Select(px =>_context.Sizes.Single(pe=>px.Id==pe.Id)).ToList();
            edit.Colors = product.Colors.Select(px =>_context.Colors.Single(pe=>px.Id==pe.Id)).ToList();
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
                ProductImageUrl = savedEntity.ProductImageUrl,
                ProductFeatured = savedEntity.ProductFeatured,
            };
        }
    }
}